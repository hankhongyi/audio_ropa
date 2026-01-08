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
using AudioRopa.Model;

namespace AudioRopa.Pages
{
    public partial class AprPage : Page
    {
        private readonly AptCommunicator aptCommunicator = AptCommunicator.Instance;
        public AprPage()
        {
            InitializeComponent();
            aptCommunicator.OnAprSettingTransferClicked += HandleTransferClciked;
            aptCommunicator.OnAptTransferCancelled += OnTransferCancelled;
        }
        
        private void HandleTransferClciked()
        {
            if (AprSection2.Visibility == Visibility.Collapsed)
            {
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
    }
}
