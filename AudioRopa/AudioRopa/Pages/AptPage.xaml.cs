using AudioRopa.Bluetooth;
using AudioRopa.Model;
using System.Diagnostics;
using System.Windows.Controls;

namespace AudioRopa.Pages
{
    public partial class AptPage : Page
    {
        private readonly AppCommunicator aptCommunicator = AppCommunicator.Instance;
        private DeviceState deviceState = DeviceState.Instance;
        public AptPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            deviceState.ConnectDongle();
        }

        private void OnSettingClicked()
        {
            Debug.WriteLine("- OnSettingClicked -");
            AptSettings.Visibility = System.Windows.Visibility.Visible;
            AptSettings.AprTransfer.Visibility = System.Windows.Visibility.Collapsed;
            AptSettings.AptInfoSetting.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnConnectClicked(AprInfo aprInfo)
        {
            AptSettings.Visibility = System.Windows.Visibility.Visible;
            AptSettings.AprTransfer.Visibility = System.Windows.Visibility.Visible;
            AptSettings.AptInfoSetting.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void OnAptInfoSettingCancelled()
        {
            if (AptSettings.AptInfoSetting.Visibility == System.Windows.Visibility.Visible)
            {
                AptSettings.AptInfoSetting.Visibility = System.Windows.Visibility.Collapsed;
                AptSettings.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void OnAprTransferCancelled()
        {
            if (AptSettings.AprTransfer.Visibility == System.Windows.Visibility.Visible)
            {
                AptSettings.AprTransfer.Visibility = System.Windows.Visibility.Collapsed;
                AptSettings.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            aptCommunicator.OnAptSettingClicked += OnSettingClicked;
            aptCommunicator.OnAptConnectClicked += OnConnectClicked;
            aptCommunicator.OnAptSettingCancelled += OnAptInfoSettingCancelled;
            aptCommunicator.OnAprTransferCancelled += OnAprTransferCancelled;
        }


        private void OnUnloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            aptCommunicator.OnAptSettingClicked -= OnSettingClicked;
            aptCommunicator.OnAptConnectClicked -= OnConnectClicked;
            aptCommunicator.OnAptSettingCancelled -= OnAptInfoSettingCancelled;
            aptCommunicator.OnAprTransferCancelled -= OnAprTransferCancelled;
        }
    }
}
