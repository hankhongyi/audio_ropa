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
            aptCommunicator.InvokeAprSettingTransfer();
        }

        private void OnReturnHomeClicked(object sender, RoutedEventArgs e)
        {
            aptCommunicator.InvokeReturnHoome();
        }
    }
}
