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

namespace AudioRopa.Pages
{
    public partial class AprPage : Page
    {
        private readonly AptCommunicator aptCommunicator = AptCommunicator.Instance;
        private AprInfo aprInformation = new AprInfo();
        private AprOperator aprOperator = new AprOperator();
        public AprPage()
        {
            InitializeComponent();
            aptCommunicator.OnAprSettingTransferClicked += HandleSettingTransferClciked;
            aptCommunicator.OnAprTransferCancelled += OnTransferCancelled;
            aptCommunicator.OnAprTransferClicked += OnTransferClicked;
        }
        
        private void HandleSettingTransferClciked(AprInfo aprInfo)
        {
            if (AprSection2.Visibility == Visibility.Collapsed)
            {
                aprInformation = aprInfo;
                AprSection2.Visibility = Visibility.Visible;
            }
        }

        private void OnTransferCancelled()
        {
            if (AprSection2.Visibility == Visibility.Visible)
            {
                AprSection2.Visibility = Visibility.Collapsed;
            }
        }

        private void OnTransferClicked()
        {
            Debug.WriteLine("aprInformation:" + aprInformation);
            if (aprInformation.ChannelName != string.Empty && aprInformation.Password != string.Empty)
            {
                Debug.WriteLine("information is OK");
                aprOperator.write(aprInformation);
            }
        }
    }
}
