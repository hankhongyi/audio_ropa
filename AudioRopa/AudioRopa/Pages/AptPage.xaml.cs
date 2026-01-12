using System.Windows.Controls;
using AudioRopa.Model;
using System.Diagnostics;

namespace AudioRopa.Pages
{
    public partial class AptPage : Page
    {
        private readonly AptCommunicator aptCommunicator = AptCommunicator.Instance;

        public AptPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnSettingClicked()
        {
            Debug.WriteLine("- OnSettingClicked -");
            AptSettings.Visibility = System.Windows.Visibility.Visible;
            AptSettings.AprTransfer.Visibility = System.Windows.Visibility.Collapsed;
            AptSettings.AptInfoSetting.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnConnectClicked()
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
            aptCommunicator.OnSettingClicked += OnSettingClicked;
            aptCommunicator.OnConnectClicked += OnConnectClicked;
            aptCommunicator.OnAptInfoSettingCancelled += OnAptInfoSettingCancelled;
            aptCommunicator.OnAprTransferCancelled += OnAprTransferCancelled;
        }


        private void OnUnloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            aptCommunicator.OnSettingClicked -= OnSettingClicked;
            aptCommunicator.OnConnectClicked -= OnConnectClicked;
            aptCommunicator.OnAptInfoSettingCancelled -= OnAptInfoSettingCancelled;
            aptCommunicator.OnAprTransferCancelled -= OnAprTransferCancelled;
        }
    }
}
