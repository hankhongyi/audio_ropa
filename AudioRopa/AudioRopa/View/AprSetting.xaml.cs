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
using AudioRopa.Model;

namespace AudioRopa.View
{
    public partial class AprSetting : UserControl
    {
        private readonly AptCommunicator aptCommunicator = AptCommunicator.Instance;
        public AprSetting()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnSaveDataClick(object sender, RoutedEventArgs e)
        {
        }

        private void OnPasteDataClick(object sender, RoutedEventArgs e)
        {
        }

        private void OnTransferClicked(object sender, RoutedEventArgs e)
        {
            AprInfo aprInfo = collectAprInfo();
            aptCommunicator.InvokeAprSettingTransfer(aprInfo);
        }

        private void OnReturnHomeClicked(object sender, RoutedEventArgs e)
        {
            aptCommunicator.InvokeReturnHome();
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            PortComboBox.ItemsSource = ports;
        }

        private AprInfo collectAprInfo()
        {
            string portName = PortComboBox.SelectedItem?.ToString() ?? string.Empty;
            string channelName = AuracastChannelNameInput.Text;
            string password = AuracastPasswordInput.Text;
            AprInfo aprInfo = new AprInfo();
            aprInfo.Port = portName;
            aprInfo.ChannelName = channelName;
            aprInfo.Password = password;
            return aprInfo;
        }
    }
}
