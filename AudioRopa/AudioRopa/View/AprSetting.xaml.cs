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
    public partial class AprSetting : UserControl
    {
        private readonly AppCommunicator appCommunicator = AppCommunicator.Instance;

        public AprSetting()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnLoaded;
        }

        private void OnSaveDataClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ChannelName = AuracastChannelNameInput.Text;
            Properties.Settings.Default.ChannelPassword = AuracastPasswordInput.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show(Properties.Resources.Message_AprDataSaved, Properties.Resources.MessageTitle, MessageBoxButton.OK);
        }

        private void OnPasteDataClick(object sender, RoutedEventArgs e)
        {
            AuracastChannelNameInput.Text = Properties.Settings.Default.ChannelName;
            AuracastPasswordInput.Text = Properties.Settings.Default.ChannelPassword;
        }

        private void OnTransferClicked(object sender, RoutedEventArgs e)
        {
            AprInfo aprInfo = CollectAprInfo();
            appCommunicator.InvokeAprSettingTransfer(aprInfo);
        }

        private void OnReturnHomeClicked(object sender, RoutedEventArgs e)
        {
            appCommunicator.InvokeReturnHome();
        }

        private void AuracastChannelNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = AuracastChannelNameInput.Text;
            AprInfo aprInfo = CollectAprInfo();
            appCommunicator.InvokeAprChannelNameChanged(aprInfo);
        }

        private void AuracastPasswordInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = AuracastPasswordInput.Text;
            AprInfo aprInfo = CollectAprInfo();
            appCommunicator.InvokeAprPasswordChanged(aprInfo);
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


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
        }


        private void OnUnLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
