using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRopa.Model
{
    public class AprInfo
    {
        private string channelName;
        private string password;
        private string portName;

        public string ChannelName
        {
            get { return channelName; }
            set { channelName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Port
        {
            get { return portName; }
            set { portName = value; }
        }
    }
}
