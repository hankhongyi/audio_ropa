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

namespace AudioRopa.View
{
    public partial class AptConnector : UserControl
    {
        public AptConnector()
        {
            InitializeComponent();
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateProgressIndicator();
            if (sender is Slider slider)
            {
                int currentProgress = (int)slider.Value;
                Debug.WriteLine($"- VolumeSlider_MouseUp - Current Progress: {currentProgress} -");
            }
        }

        private void VolumeSlider_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateProgressIndicator();
        }

        private void VolumeSlider_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateProgressIndicator();
        }

        private void UpdateProgressIndicator()
        {
            if (VolumeSlider == null || ProgressTrack == null || ProgressIndicator == null || ProgressText == null)
                return;

            try
            {
                // Calculate progress width based on slider value
                double sliderWidth = VolumeSlider.ActualWidth;
                double progressRatio = VolumeSlider.Value / VolumeSlider.Maximum;
                double progressWidth = sliderWidth * progressRatio;

                // Update progress track width
                ProgressTrack.Width = progressWidth;

                // Update progress indicator position
                double indicatorPosition = progressWidth - (ProgressIndicator.Width / 2);
                if (indicatorPosition < 0) indicatorPosition = 0;
                if (indicatorPosition > sliderWidth - ProgressIndicator.Width)
                    indicatorPosition = sliderWidth - ProgressIndicator.Width;

                ProgressIndicator.Margin = new Thickness(indicatorPosition, -18, 0, 0);

                // Update progress text
                ProgressText.Text = ((int)VolumeSlider.Value).ToString();
            }
            catch (Exception ex)
            {
                // Log error or handle gracefully
                Debug.WriteLine($"Error updating progress indicator: {ex.Message}");
            }
        }

        private void OnConnectButtonClicked(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("On Connect Button Clicked");
        }
    }
}
