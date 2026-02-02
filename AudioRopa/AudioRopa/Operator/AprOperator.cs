using AudioRopa.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace AudioRopa.Operator
{
    public class AprOperator
    {
        private SerialPort _serialPort;
        private Queue<byte[]> _byteArrayQueue;
        private readonly object _queueLock = new object();
        private Thread _commandThread;
        private CancellationTokenSource _cancellationTokenSource;

        public event Action OnAprTransferStared;
        public event Action OnAprClosingPort;
        public event Action OnAprTransferCompleted;
        public event Action<string> OnAprTransferError;

        public AprOperator()
        {
            _byteArrayQueue = new Queue<byte[]>();
            _serialPort = new SerialPort();
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }
        
        public void write(AprInfo aprInfo)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = _cancellationTokenSource.Token;
            
            _commandThread = new Thread(() =>
            {
                try
                {
                    token.ThrowIfCancellationRequested();
                    OnAprTransferStared?.Invoke();
                    
                    token.ThrowIfCancellationRequested();
                    PrepareCommand(aprInfo);
                    
                    token.ThrowIfCancellationRequested();
                    ConfigurePort(aprInfo.Port);
                    
                    token.ThrowIfCancellationRequested();
                    if (OpenConnection())
                    {
                        SendCommands(token);
                    }
                }
                catch (OperationCanceledException)
                {
                    Debug.WriteLine("Transfer cancelled");
                    Close();
                    OnAprTransferError?.Invoke("Transfer cancelled by user");
                }
            });
            _commandThread.IsBackground = true;
            _commandThread.Start();
        }

        public void SetAprAuracastOnOff(AprInfo aprInfo, bool isOn)
        {
            _commandThread = new Thread(() =>
            {
                try
                {
                    ConfigurePort(aprInfo.Port);
                    if (OpenConnection())
                    {
                        byte[] command = RetrieveSetAuracastOnOffCommand(isOn);
                        SendCommand(command);
                        TriggerPortClosingActions();
                    }
                }
                catch (OperationCanceledException)
                {
                    Debug.WriteLine("Transfer cancelled");
                    Close();
                    OnAprTransferError?.Invoke("Transfer cancelled by user");
                }
            });

            _commandThread.IsBackground = true;
            _commandThread.Start();
        }

        private void ConfigurePort(string portName)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
            Debug.WriteLine("Configuring port: " + portName);
            if (portName == string.Empty)
            {
                OnAprTransferError?.Invoke("Invalid port name.");
                return;
            }
            // Standard UART Configuration
            _serialPort.PortName = portName; // e.g., "COM3"
            _serialPort.BaudRate = 3000000; // e.g., 9600, 115200
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Handshake = Handshake.None;

            // Set timeouts to avoid the UI hanging if the device doesn't respond
            _serialPort.ReadTimeout = 1000;
            _serialPort.WriteTimeout = 1000;
        }

        private bool OpenConnection()
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _serialPort.Open();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error opening port: {ex.Message}");
                OnAprTransferError?.Invoke("Error opening port: " + ex.Message);
                return false;
            }
        }

        private void SendCommands(CancellationToken token)
        {
            byte[] command = null;
            int size = _byteArrayQueue.Count;
            for (int i = 0; i < size; i++)
            {
                token.ThrowIfCancellationRequested();
                
                command = _byteArrayQueue.Dequeue();
                Debug.WriteLine("command:" + BitConverter.ToString(command));
                SendCommand(command);
                Thread.Sleep(200); // Wait between commands
            }
            TriggerPortClosingActions();
        }

        private void TriggerPortClosingActions()
        {
            OnAprClosingPort?.Invoke();
            Thread.Sleep(200);
            Close();
            OnAprTransferCompleted?.Invoke();
        }

        private void ExecuteCommand()
        {
            byte[] command = null;
            lock (_queueLock)
            {
                if (_byteArrayQueue.Count > 0)
                {
                    command = _byteArrayQueue.Dequeue();
                }
            }

            if (command != null)
            {
                Debug.WriteLine("command:" + BitConverter.ToString(command));
                SendCommand(command);
            }
            else
            {
                Close();
            }
        }

        private void SendCommand(byte[] command)
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Write(command, 0, command.Length);
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            int bytesToRead = sp.BytesToRead;
            byte[] buffer = new byte[bytesToRead];
            sp.Read(buffer, 0, bytesToRead);
            
            //Convert to hex string for debugging
            string hex = BitConverter.ToString(buffer);
            Debug.WriteLine("response:" + hex);
            
            // Search for set response pattern 05-5B-05-00-12-20
            byte[] pattern = new byte[] { 0x05, 0x5B, 0x05, 0x00, 0x12, 0x20 };
            int patternIndex = FindPattern(buffer, pattern);

            if (patternIndex != -1 && patternIndex + pattern.Length + 3 <= buffer.Length)
            {
                // Extract 3 bytes after the pattern
                byte byte1 = buffer[patternIndex + pattern.Length];
                byte byte2 = buffer[patternIndex + pattern.Length + 1];
                byte byte3 = buffer[patternIndex + pattern.Length + 2];

                string extractedBytes = $"{byte1:X2}-{byte2:X2}-{byte3:X2}";
                Debug.WriteLine($"Found pattern at index {patternIndex}, next 3 bytes: {extractedBytes}");
            }
        }

        private int FindPattern(byte[] buffer, byte[] pattern)
        {
            for (int i = 0; i <= buffer.Length - pattern.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (buffer[i + j] != pattern[j])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    return i;
                }
            }
            return -1; // Pattern not found
        }

        private void PrepareCommand(AprInfo aprInfo)
        {
            _byteArrayQueue.Clear();
            //Turn on auracast priority
            byte[] auracast_priority = RetrieveSetAuracastOnOffCommand(true);

            //0x05, 0x5A, 0x06, 0x00, 0x12, 0x20, 0x60, 0x00, 0x00, 0x01
            //0x05, 0x5A, 0x06, 0x00, 0x12, 0x20, 0x62, 0x00, 0x00, name_length, channel_name_bytes...
            //0x05, 0x5A, 0x06, 0x00, 0x12, 0x20, 0x64, 0x00, 0x00, password_length, password_bytes...

            //Set channel name
            int totalNameLength = 6 + aprInfo.ChannelName.Length;
            int nameLength = aprInfo.ChannelName.Length;
            byte totalNameLengthByte = (byte)totalNameLength;
            byte nameLengthByte = (byte)nameLength;
            byte[] channelNameBytes = Encoding.UTF8.GetBytes(aprInfo.ChannelName);
            Debug.WriteLine("channelNameBytes:" + BitConverter.ToString(channelNameBytes));

            byte[] auracast_channel_name = new byte[10 + nameLength];
            auracast_channel_name[0] = 0x05;
            auracast_channel_name[1] = 0x5A;
            auracast_channel_name[2] = totalNameLengthByte;
            auracast_channel_name[3] = 0x00;
            auracast_channel_name[4] = 0x12;
            auracast_channel_name[5] = 0x20;
            auracast_channel_name[6] = 0x62;
            auracast_channel_name[7] = 0x00;
            auracast_channel_name[8] = 0x00;
            auracast_channel_name[9] = nameLengthByte;
            Array.Copy(channelNameBytes, 0, auracast_channel_name, 10, nameLength);

            //Set password
            int totalPasswordLength = 6 + aprInfo.Password.Length;
            int passwordLength = aprInfo.Password.Length;
            byte totalPasswordLengthByte = (byte)totalPasswordLength;
            byte passwordLengthByte = (byte)passwordLength;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(aprInfo.Password);
            Debug.WriteLine("passwordBytes:" + BitConverter.ToString(passwordBytes));

            byte[] auracast_password = new byte[10 + passwordLength];
            auracast_password[0] = 0x05;
            auracast_password[1] = 0x5A;
            auracast_password[2] = totalPasswordLengthByte;
            auracast_password[3] = 0x00;
            auracast_password[4] = 0x12;
            auracast_password[5] = 0x20;
            auracast_password[6] = 0x64;
            auracast_password[7] = 0x00;
            auracast_password[8] = 0x00;
            auracast_password[9] = passwordLengthByte;
            Array.Copy(passwordBytes, 0, auracast_password, 10, passwordLength);

            lock (_queueLock)
            {
                _byteArrayQueue.Enqueue(auracast_priority);
                _byteArrayQueue.Enqueue(auracast_channel_name);
                _byteArrayQueue.Enqueue(auracast_password);
            }
        }

        private byte[] RetrieveSetAuracastOnOffCommand(bool isOn)
        {
            if (isOn) {
                return new byte[] { 0x05, 0x5A, 0x06, 0x00, 0x12, 0x20, 0x60, 0x00, 0x00, 0x01 };
            }
            else
            {
                return new byte[] { 0x05, 0x5A, 0x06, 0x00, 0x12, 0x20, 0x60, 0x00, 0x00, 0x00 };
            }
        }

        private void Close()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
                Debug.WriteLine("Serial port closed.");
            }
        }

        public void CancelTransfer()
        {
            _cancellationTokenSource?.Cancel();
        }
    }

}
