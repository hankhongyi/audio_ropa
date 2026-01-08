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

namespace AudioRopa.Custom
{
    /// <summary>
    /// CustomButton.xaml 的互動邏輯
    /// </summary>
    public partial class CustomButton : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CustomButton), new PropertyMetadata(string.Empty));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public event RoutedEventHandler Click;

        public CustomButton()
        {
            InitializeComponent();
            this.MouseLeftButtonUp += CustomButton_MouseLeftButtonUp;
        }

        private void CustomButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Click?.Invoke(this, new RoutedEventArgs());
        }
    }
}
