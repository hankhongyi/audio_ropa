using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using System.Diagnostics;
using AudioRopa.Model;
using AudioRopa.Operator;

namespace AudioRopa.View
{
    public partial class AprTransfer : UserControl
    {
        private bool _isInitialized = false;
        private bool _isVisible = false;
        private readonly AppCommunicator appCommunicator = AppCommunicator.Instance;
        private AprInfo aprInformation;
        private AprOperator aprOperator = new AprOperator();

        public AprTransfer()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnTransferClicked(object sender, RoutedEventArgs e)
        {
            TransferStatusText.Text = "";
            string portName = PortComboBox.SelectedItem?.ToString() ?? string.Empty;
            Debug.WriteLine("portName:" + portName);
            if (aprInformation != null && portName != string.Empty &&
                aprInformation.ChannelName != string.Empty && aprInformation.Password != string.Empty)
            {
                aprInformation.Port = portName;
                Debug.WriteLine("aprInformation.Port:" + aprInformation.Port);
                DisableButtons();
                Debug.WriteLine("ChannelName:" + aprInformation.ChannelName);
                Debug.WriteLine("Password:" + aprInformation.Password);
                //Execute a sequence of commands by calling write function.
                aprOperator.write(aprInformation);
            }
            else
            {
                if (aprInformation == null || portName == string.Empty)
                {
                    TransferStatusText.Text = Properties.Resources.Error_Apr_Not_Connect;
                }
                if (aprInformation.ChannelName == string.Empty || aprInformation.Password == string.Empty)
                {
                    TransferStatusText.Text = Properties.Resources.Error_Apr_String_Empty;
                }
            }
        }

        private void OnResetClicked(object sender, RoutedEventArgs e)
        {
            TransferStatusText.Text = "";
            string portName = PortComboBox.SelectedItem?.ToString() ?? string.Empty;
            Debug.WriteLine("portName:" + portName);
            if (aprInformation != null && portName != string.Empty)
            {
                aprInformation.Port = portName;
                Debug.WriteLine("aprInformation.Port:" + aprInformation.Port);
                DisableButtons();
                //Execute turn auracast off command.
                aprOperator.SetAprAuracastOnOff(aprInformation, false);
            }
            else
            {
                if (aprInformation == null || portName == string.Empty)
                {
                    TransferStatusText.Text = Properties.Resources.Error_Apr_Not_Connect;
                }
            }
        }

        private void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("On Cancel Button Clicked");
            aprOperator.CancelTransfer();
            appCommunicator.InvokeAprTransferCancelled();
        }

        private void HandleSettingTransferClicked(AprInfo aprInfo)
        {
            aprInformation = aprInfo;
            TransferButton.Visibility = Visibility.Visible;
            ResetButtons.Visibility = Visibility.Collapsed;
        }

        private void HandleSettingResetClicked(AprInfo aprInfo)
        {
            aprInformation = aprInfo;
            TransferButton.Visibility = Visibility.Collapsed;
            ResetButtons.Visibility = Visibility.Visible;
        }

        private void HandleChannelNameChanged(AprInfo aprInfo)
        {
            if (_isVisible)
            {
                aprInformation = aprInfo;
            }
        }

        private void HandlePasswordChanged(AprInfo aprInfo)
        {
            if (_isVisible)
            {
                aprInformation = aprInfo;
            }
        }

        private void HandleAptConnectClicked(AprInfo aprInfo)
        {
            //Coming from APT page
            aprInformation = aprInfo;
        }

        private void ConfigPort()
        {
            string[] ports = SerialPort.GetPortNames();

            // Set the ItemsSource
            PortComboBox.ItemsSource = ports;

            // Optionally select the first item
            if (ports.Length > 0)
            {
                PortComboBox.SelectedIndex = 0;
            }

            //Test code
            //string[] sample = new string[] { "COM1", "COM2", "COM3" };
            //PortComboBox.ItemsSource = sample;
            //PortComboBox.SelectedIndex = 0;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_isInitialized)
            {
                return;
            }
            _isInitialized = true;
            appCommunicator.OnAprSettingTransferClicked += HandleSettingTransferClicked;
            appCommunicator.OnAprSettingResetClicked += HandleSettingResetClicked;
            appCommunicator.OnAprChannelNameChanged += HandleChannelNameChanged;
            appCommunicator.OnAprPassowrdChanged += HandlePasswordChanged;
            appCommunicator.OnAptConnectClicked += HandleAptConnectClicked;
            aprOperator.OnAprTransferStared += OnTransferStarted;
            aprOperator.OnAprClosingPort += OnTransferClosingPort;
            aprOperator.OnAprTransferCompleted += OnTransferCompleted;
            aprOperator.OnAprTransferError += OnTransferError;
            IsVisibleChanged += OnVisibilityChanged;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            appCommunicator.OnAprSettingTransferClicked -= HandleSettingTransferClicked;
            appCommunicator.OnAprSettingResetClicked -= HandleSettingResetClicked;
            appCommunicator.OnAprChannelNameChanged -= HandleChannelNameChanged;
            appCommunicator.OnAprPassowrdChanged -= HandlePasswordChanged;
            appCommunicator.OnAptConnectClicked -= HandleAptConnectClicked;
            aprOperator.OnAprTransferStared -= OnTransferStarted;
            aprOperator.OnAprClosingPort -= OnTransferClosingPort;
            aprOperator.OnAprTransferCompleted -= OnTransferCompleted;
            aprOperator.OnAprTransferError -= OnTransferError;
            IsVisibleChanged -= OnVisibilityChanged;
        }

        private void OnVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _isVisible = (bool)e.NewValue;
            Debug.WriteLine("isVisible:" + _isVisible);
            if (_isVisible)
            {
                ConfigPort();
            }
            else
            {
                TransferStatusText.Text = "";
            }
        }

        private void OnTransferStarted()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                //Run on UI thread
                Debug.WriteLine("Transfer Started");
                TransferStatusText.Text = Properties.Resources.Transfer_Start;
            });
        }

        private void OnTransferClosingPort()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                //Run on UI thread
                Debug.WriteLine("Transfer Closing Port");
                TransferStatusText.Text = Properties.Resources.Transfer_Closing;
            });
        }

        private void OnTransferCompleted()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                //Run on UI thread
                Debug.WriteLine("Transfer Completed");
                TransferStatusText.Text = Properties.Resources.Transfer_Completed;
                EnableButtons();
            });
        }

        private void OnTransferError(string errorMessage)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                //Run on UI thread
                string messagePrefix = "Transfer Error: ";
                string fullMessage = messagePrefix + errorMessage;
                Debug.WriteLine(fullMessage);
                TransferStatusText.Text = errorMessage;
                EnableButtons();
            });
        }

        private void EnableButtons()
        {
            if (TransferButton.Visibility == Visibility.Visible)
            {
                TransferButton.IsEnabled = true;
            }
            else if (ResetButtons.Visibility == Visibility.Visible)
            {
                ResetButtons.IsEnabled = true;
            }

            CancelButton.IsEnabled = true;
        }

        private void DisableButtons()
        {
            if (TransferButton.Visibility == Visibility.Visible)
            {
                TransferButton.IsEnabled = false;
            }
            else if (ResetButtons.Visibility == Visibility.Visible)
            {
                ResetButtons.IsEnabled = false;
            }
            CancelButton.IsEnabled = false;
        }
    }
}
