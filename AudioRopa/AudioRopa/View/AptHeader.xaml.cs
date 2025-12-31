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

namespace AudioRopa.View
{
    /// <summary>
    /// AptHeader.xaml 的互動邏輯
    /// </summary>
    public partial class AptHeader : UserControl
    {
        public AptHeader()
        {
            InitializeComponent();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Find the parent page's NavigationService to navigate to SettingsPage
            var page = FindParent<Page>(this);
            if (page?.NavigationService != null)
            {
                //page.NavigationService.Navigate(new SettingsPage());
            }
        }

        // Helper method to find parent of specific type
        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = System.Windows.Media.VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }
    }
}
