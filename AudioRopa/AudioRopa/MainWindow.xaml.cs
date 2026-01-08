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
using AudioRopa.Pages;

namespace AudioRopa
{
    public partial class MainWindow : Window
    {
        private readonly AptCommunicator aptCommunicator = AptCommunicator.Instance;
        public MainWindow()
        {
            InitializeComponent();
            aptCommunicator.OnReturnHomeClicked += HandleReturnHome;
        }

        private void HandleReturnHome()
        {
            // Navigate to DeviceSelectionPage
            MainFrame.Navigate(new DeviceSelectionPage());
            
            // Clear the back stack
            while (MainFrame.CanGoBack)
            {
                MainFrame.RemoveBackEntry();
            }
        }
    }
}
