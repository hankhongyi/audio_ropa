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

namespace AudioRopa.View
{
    public partial class AptInfoSetting : UserControl
    {
        private readonly AptCommunicator aptCommunicator = AptCommunicator.Instance;

        public AptInfoSetting()
        {
            InitializeComponent();
            aptCommunicator.OnAgcOnOffChanged += HandleAgcOnOff;
            aptCommunicator.OnAuracastInfoRead += HandleAuracasstInfoRead;
        }

        private void AGCSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateAGCProgressIndicator();
        }

        private void AGCSlider_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateAGCProgressIndicator();
        }

        private void AGCSlider_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateAGCProgressIndicator();
        }

        private void UpdateAGCProgressIndicator()
        {
            if (AGCSlider == null || ProgressTrack2 == null)
                return;

            try
            {
                // Calculate progress width based on slider value
                double sliderWidth = AGCSlider.ActualWidth;
                double progressRatio = AGCSlider.Value / AGCSlider.Maximum;
                double progressWidth = sliderWidth * progressRatio;

                // Update progress track width
                ProgressTrack2.Width = progressWidth;
            }
            catch (Exception ex)
            {
                // Log error or handle gracefully
                Debug.WriteLine($"Error updating game/voice balance progress indicator: {ex.Message}");
            }
        }

        private void HandleAgcOnOff(bool isOn)
        {
            Debug.WriteLine("is AGC on:" + isOn);
        }


        private void OnUpdagteClicked(object sender, RoutedEventArgs e)
        {
            aptCommunicator.InvokeUpdate();
        }

        private void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            aptCommunicator.InvokeAptInfoSettingCancelled();
        }

        private void HandleAuracasstInfoRead(AuracastInfo auracastInfo)
        {
            AuracastChannelNameInput.Text = auracastInfo.ChannelName;
            AuracastPasswordInput.Text = auracastInfo.Password;
            AuracastTransmissionQualityInput.Text = auracastInfo.Quality;
            AGCControl.AgcSwitch.IsOn = auracastInfo.Agc;
            AGCSlider.Value = auracastInfo.TxPower;
        }
    }
}
