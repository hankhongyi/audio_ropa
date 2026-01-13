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

namespace AudioRopa.View
{
    public partial class AprSetting : UserControl
    {
        private readonly AptCommunicator aptCommunicator = AptCommunicator.Instance;
        public AprSetting()
        {
            InitializeComponent();
        }

        private void OnSaveDataClick(object sender, RoutedEventArgs e)
        {
        }

        private void OnPasteDataClick(object sender, RoutedEventArgs e)
        {
        }

        private void OnTransferClicked(object sender, RoutedEventArgs e)
        {
            AprInfo aprInfo = CollectAprInfo();
            aptCommunicator.InvokeAprSettingTransfer(aprInfo);
        }

        private void OnReturnHomeClicked(object sender, RoutedEventArgs e)
        {
            aptCommunicator.InvokeReturnHome();
        }

        private void AuracastChannelNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = AuracastChannelNameInput.Text;
            AprInfo aprInfo = CollectAprInfo();
            aptCommunicator.InvokeAprChannelNameChanged(aprInfo);
        }

        private void AuracastPasswordInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = AuracastPasswordInput.Text;
            AprInfo aprInfo = CollectAprInfo();
            aptCommunicator.InvokeAprPasswordChanged(aprInfo);
        }

        private AprInfo CollectAprInfo()
        {
            string portName = "";
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
