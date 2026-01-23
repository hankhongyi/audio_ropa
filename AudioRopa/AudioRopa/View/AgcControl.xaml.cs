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

namespace AudioRopa.View
{
    public partial class AgcControl : UserControl
    {
        private readonly AppCommunicator AptCommunicator = AppCommunicator.Instance;
        public AgcControl()
        {
            InitializeComponent();
        }

        private void AGCSwitchButton_StateChanged(object sender, bool isOn)
        {
            // Handle the switch state change
            AptCommunicator.InvokeAgcOnOff(isOn);
        }

        private void AutoGainInfo_Click(object sender, MouseButtonEventArgs e)
        {
            // Handle the info icon click
            // For example, show information about the Auto Gain feature
        }
    }
}
