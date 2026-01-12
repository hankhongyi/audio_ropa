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
using System.Diagnostics;
using AudioRopa.Model;
using AudioRopa.Operator;

namespace AudioRopa.View
{
    public partial class AprTransfer : UserControl
    {
        private readonly AptCommunicator aptCommunicator = AptCommunicator.Instance;
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
            Debug.WriteLine("On Transfer Button Clicked");
            if (aprInformation != null && aprInformation.ChannelName != string.Empty && aprInformation.Password != string.Empty)
            {
                Debug.WriteLine("information is OK");
                aprOperator.write(aprInformation);
            }
            else
            {
                Debug.WriteLine("- aprInformation is null or contains empty string -");
            }
        }

        private void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("On Cancel Button Clicked");
            aptCommunicator.InvokeAprTransferCancelled();
        }

        private void HandleSettingTransferClciked(AprInfo aprInfo)
        {
            aprInformation = aprInfo;
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //Seems like OnLoaded is called multiple times, so unsubscript previous subscription first.
            aptCommunicator.OnAprSettingTransferClicked -= HandleSettingTransferClciked;
            aptCommunicator.OnAprSettingTransferClicked += HandleSettingTransferClciked;
        }

        private void OnUnloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            aptCommunicator.OnAprSettingTransferClicked -= HandleSettingTransferClciked;
        }
    }
}
