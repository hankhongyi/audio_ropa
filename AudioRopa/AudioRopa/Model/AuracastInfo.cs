using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRopa.Model
{
    public class AuracastInfo
    {
        private string channelName;
        private string password;
        private string quality;
        private bool agc;
        private int txPower;

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

        public string Quality
        {
            get { return quality; }
            set { quality = value; }
        }

        public bool Agc
        {
            get { return agc; }
            set { agc = value; }
        }

        public int TxPower
        {
            get { return txPower; }
            set { txPower = value; }
        }
    }
}
