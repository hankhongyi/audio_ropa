using System.Windows.Controls;
using AudioRopa.Model;
using System.Diagnostics;

namespace AudioRopa.Pages
{
    public partial class AptPage : Page
    {
        private readonly AptCommunicator AptCommunicator = AptCommunicator.Instance;
        
        public AptPage()
        {
            InitializeComponent();
            AptCommunicator.OnSettingClicked += OnSettingClicked;
            AptCommunicator.OnConnectClicked += OnConnectClicked;
            AptCommunicator.OnAptInfoSettingCancelled += OnAptInfoSettingCancelled;
            AptCommunicator.OnAptTransferCancelled += OnAptTransferCancelled;
        }

        private void OnSettingClicked()
        {
            Debug.WriteLine("- OnSettingClicked -");
            AptSettings.Visibility = System.Windows.Visibility.Visible;
            AptSettings.AptTransfer.Visibility = System.Windows.Visibility.Collapsed;
            AptSettings.AptInfoSetting.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnConnectClicked()
        {
            AptSettings.Visibility = System.Windows.Visibility.Visible;
            AptSettings.AptTransfer.Visibility = System.Windows.Visibility.Visible;
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

        private void OnAptTransferCancelled()
        {
            if (AptSettings.AptTransfer.Visibility == System.Windows.Visibility.Visible)
            {
                AptSettings.AptTransfer.Visibility = System.Windows.Visibility.Collapsed;
                AptSettings.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
