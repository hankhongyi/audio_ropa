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
        private readonly AppCommunicator aptCommunicator = AppCommunicator.Instance;
        public AprPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }
        
        private void HandleSettingTransferClciked(AprInfo aprInfo)
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


        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            aptCommunicator.OnAprSettingTransferClicked += HandleSettingTransferClciked;
            aptCommunicator.OnAprTransferCancelled += OnTransferCancelled;
        }

        private void OnUnloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            aptCommunicator.OnAprSettingTransferClicked -= HandleSettingTransferClciked;
            aptCommunicator.OnAprTransferCancelled -= OnTransferCancelled;
        }
    }
}
