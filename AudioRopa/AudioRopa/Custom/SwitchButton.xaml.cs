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
using System.Windows.Media.Animation;
namespace AudioRopa.Custom
{
    public partial class SwitchButton : UserControl
    {
        public static readonly DependencyProperty IsOnProperty =
            DependencyProperty.Register("IsOn", typeof(bool), typeof(SwitchButton),
                new PropertyMetadata(false, OnIsOnChanged));

        public bool IsOn
        {
            get { return (bool)GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }

        public event EventHandler<bool> StateChanged;

        public SwitchButton()
        {
            InitializeComponent();
            UpdateVisualState(false);
        }

        private static void OnIsOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var switchButton = d as SwitchButton;
            switchButton?.UpdateVisualState((bool)e.NewValue);
        }

        private void SwitchButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsOn = !IsOn;
            StateChanged?.Invoke(this, IsOn);
        }

        private void UpdateVisualState(bool isOn)
        {
            if (isOn)
            {
                // On state - circle on right, "on" text on left
                var onImageBrush = new ImageBrush();
                onImageBrush.ImageSource = new System.Windows.Media.Imaging.BitmapImage(
                    new Uri("pack://application:,,,/Images/switch_on_bg.png"));
                BackgroundBorder.Background = onImageBrush;

                OnText.Visibility = Visibility.Visible;   // Show "on" text on left
                OffText.Visibility = Visibility.Collapsed; // Hide "off" text

                // Animate circle to right position
                var animation = new DoubleAnimation
                {
                    To = 30, // Move 30 pixels to the right (54 - 24 = 30)
                    Duration = TimeSpan.FromMilliseconds(200),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                };

                CircleTransform.BeginAnimation(TranslateTransform.XProperty, animation);
            }
            else
            {
                // Off state - circle on left, "off" text on right
                var offImageBrush = new ImageBrush();
                offImageBrush.ImageSource = new System.Windows.Media.Imaging.BitmapImage(
                    new Uri("pack://application:,,,/Images/switch_off_bg.png"));
                BackgroundBorder.Background = offImageBrush;

                OnText.Visibility = Visibility.Collapsed; // Hide "on" text
                OffText.Visibility = Visibility.Visible;  // Show "off" text on right

                // Animate circle to left position
                var animation = new DoubleAnimation
                {
                    To = 0, // Move to left position
                    Duration = TimeSpan.FromMilliseconds(200),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                };

                CircleTransform.BeginAnimation(TranslateTransform.XProperty, animation);
            }
        }
    }
}