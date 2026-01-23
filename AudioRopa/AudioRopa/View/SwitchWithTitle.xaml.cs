using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AudioRopa.View
{
    public partial class SwitchWithTitle : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SwitchWithTitle), new PropertyMetadata("Title"));

        public static readonly DependencyProperty InfoIconSourceProperty =
            DependencyProperty.Register("InfoIconSource", typeof(ImageSource), typeof(SwitchWithTitle), new PropertyMetadata(null));

        public static readonly DependencyProperty InfoToolTipProperty =
            DependencyProperty.Register("InfoToolTip", typeof(string), typeof(SwitchWithTitle), new PropertyMetadata(string.Empty));

        public event EventHandler<bool> SwitchStateChanged;

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public ImageSource InfoIconSource
        {
            get { return (ImageSource)GetValue(InfoIconSourceProperty); }
            set { SetValue(InfoIconSourceProperty, value); }
        }

        public string InfoToolTip
        {
            get { return (string)GetValue(InfoToolTipProperty); }
            set { SetValue(InfoToolTipProperty, value); }
        }

        public SwitchWithTitle()
        {
            InitializeComponent();
        }

        private void SwitchButton_StateChanged(object sender, bool isOn)
        {
            SwitchStateChanged?.Invoke(this, isOn);
        }

        private void InfoIcon_Click(object sender, MouseButtonEventArgs e)
        {
            // Optionally handle info icon click or expose another event
        }
    }
}
