using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO.Ports;
using AudioRopa.Model;

namespace AudioRopa.Operator
{
    public class AprOperator
    {
        private SerialPort _serialPort;
        private Queue<byte[]> _byteArrayQueue;
        private byte[] _currentComamnd;

        public AprOperator()
        {
            _byteArrayQueue = new Queue<byte[]>();
            _serialPort = new SerialPort();
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }
        
        public void write(AprInfo aprInfo)
        {
            PrepareCommand(aprInfo);
            ConfigurePort(aprInfo.Port);
            if (OpenConnection())
            {
                ExecuteCommand();
            }
        }

        private void ConfigurePort(string portName)
        {
            if (portName == string.Empty) portName = "COM3";
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
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error opening port: {ex.Message}");
                return false;
            }
        }

        private void ExecuteCommand()
        {
            if (_byteArrayQueue.Count > 0)
            {
                byte[] command = _byteArrayQueue.Dequeue();
                _currentComamnd = command;
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
            Debug.WriteLine("hex:" + hex);
            
            //Execute next command if the response is received.
            if (buffer.Length > 0 && buffer[1] == 0x5B && buffer[8] == 0x00) {
                ExecuteCommand();
            }

            // Update the UI safely
            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    // Assuming you have a TextBox named 'txtLog'
            //    txtLog.AppendText($"Received: {indata}\n");
            //});
        }

        private void PrepareCommand(AprInfo aprInfo)
        {
            _byteArrayQueue.Clear();
            //Turn on auracast priority
            byte[] auracast_priority = new byte[] { 0x05, 0x5A, 0x06, 0x00, 0x12, 0x20, 0x60, 0x00, 0x00, 0x01 };

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
            auracast_channel_name[0] = 0x05;
            auracast_channel_name[1] = 0x5A;
            auracast_channel_name[2] = totalPasswordLengthByte;
            auracast_channel_name[3] = 0x00;
            auracast_channel_name[4] = 0x12;
            auracast_channel_name[5] = 0x20;
            auracast_channel_name[6] = 0x62;
            auracast_channel_name[7] = 0x00;
            auracast_channel_name[8] = 0x00;
            auracast_channel_name[9] = passwordLengthByte;
            Array.Copy(passwordBytes, 0, auracast_password, 10, passwordLength);

            _byteArrayQueue.Enqueue(auracast_priority);
            _byteArrayQueue.Enqueue(auracast_channel_name);
            _byteArrayQueue.Enqueue(auracast_password);
        }

        private void Close()
        {
            if (_serialPort != null && _serialPort.IsOpen) _serialPort.Close();
        }
    }

}
