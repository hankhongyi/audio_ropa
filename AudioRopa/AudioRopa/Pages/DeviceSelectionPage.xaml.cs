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

namespace AudioRopa.Pages
{
    /// <summary>
    /// DeviceSelectionPage.xaml 的互動邏輯
    /// </summary>
    public partial class DeviceSelectionPage : Page
    {
        public DeviceSelectionPage()
        {
            InitializeComponent();
        }

        private void Device1Button_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to Device 1 page
            this.NavigationService.Navigate(new Uri("Pages/AptPage.xaml", UriKind.Relative));
        }

        private void Device2Button_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to Device 2 page
            this.NavigationService.Navigate(new Uri("Pages/AprPage.xaml", UriKind.Relative));
        }
    }
}
