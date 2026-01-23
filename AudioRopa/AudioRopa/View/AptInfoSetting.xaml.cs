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
        private readonly AppCommunicator aptCommunicator = AppCommunicator.Instance;

        public AptInfoSetting()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
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
            if (AGCSlider == null || ProgressTrack == null)
                return;

            try
            {
                // Calculate progress width based on slider value
                double sliderWidth = AGCSlider.ActualWidth;
                double progressRatio = AGCSlider.Value / AGCSlider.Maximum;
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
                ProgressText.Text = ((int)AGCSlider.Value).ToString();
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
            AuracastInfo auracast = ComposeAuracastInfo();
            aptCommunicator.InvokeAptSettingUpdate(auracast);
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

        private void TransmissionQuality_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Only allow digits and comma
            e.Handled = !e.Text.All(c => char.IsDigit(c) || c == ',');
        }

        private void TransmissionQuality_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
            {
                // Extract only the numbers when focused for editing
                string numbersOnly = new string(textBox.Text.Where(c => char.IsDigit(c) || c == ',').ToArray());
                textBox.Text = numbersOnly;
                textBox.SelectAll();
            }
        }

        private void TransmissionQuality_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
            {
                string numbersOnly = new string(textBox.Text.Where(char.IsDigit).ToArray());
                
                if (numbersOnly.Length >= 2)
                {
                    // Split the numbers into two parts
                    int midPoint = numbersOnly.Length / 2;
                    string firstNumber = numbersOnly.Substring(0, midPoint);
                    string secondNumber = numbersOnly.Substring(midPoint);
                    
                    // Format as "XX,KHz, YY,bit"
                    textBox.Text = $"{firstNumber}KHz, {secondNumber}bit";
                }
            }
        }

        private AuracastInfo ComposeAuracastInfo()
        {
            AuracastInfo auracast = new AuracastInfo();
            auracast.ChannelName = AuracastChannelNameInput.Text;
            auracast.Password = AuracastPasswordInput.Text;
            auracast.Quality = AuracastTransmissionQualityInput.Text;
            auracast.Agc = AGCControl.AgcSwitch.IsOn;
            auracast.TxPower = (int)AGCSlider.Value;
            return auracast;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            aptCommunicator.OnAgcOnOffChanged += HandleAgcOnOff;
            aptCommunicator.OnAuracastInfoRead += HandleAuracasstInfoRead;

            // Setup transmission quality input formatting
            AuracastTransmissionQualityInput.PreviewTextInput += TransmissionQuality_PreviewTextInput;
            AuracastTransmissionQualityInput.GotFocus += TransmissionQuality_GotFocus;
            AuracastTransmissionQualityInput.LostFocus += TransmissionQuality_LostFocus;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            aptCommunicator.OnAgcOnOffChanged -= HandleAgcOnOff;
            aptCommunicator.OnAuracastInfoRead -= HandleAuracasstInfoRead;

            // Setup transmission quality input formatting
            AuracastTransmissionQualityInput.PreviewTextInput -= TransmissionQuality_PreviewTextInput;
            AuracastTransmissionQualityInput.GotFocus -= TransmissionQuality_GotFocus;
            AuracastTransmissionQualityInput.LostFocus -= TransmissionQuality_LostFocus;
        }
    }
}

