using System;
using System.Text;
namespace AudioRopa.Bluetooth
{
    public static class Commands
    {
        public static string GET_MIC_MUTE = "055A060012208C000000";
        public static string GET_MIC_MUTE_RESPONE_PREFIX = "80055B050012208C00";
        public static string GET_SURROUND_SOUND = "055A06001220AB000000";
        public static string GET_SURROUND_SOUND_RESPONSE_PREFIX = "80055B05001220AB00";
        public static string SET_SURROUND_SOUND_OFF = "055A06001220AA000000";
        public static string SET_SURROUND_SOUND_CLEAR = "055A06001220AA000001";
        public static string SET_SURROUND_SOUND_SOFT = "055A06001220AA000002";
        public static string SET_SURROUND_RESPONSE = "80055B05001220AA0000";
        
        public static string GET_ELEVATION = "055A06001220AF000000";
        public static string GET_ELEVATION_RESPONSE_PREFIX = "80055B05001220AF00";
        public static string SET_ELEVATION_TOP = "055A06001220AE000000";
        public static string SET_ELEVATION_MID = "055A06001220AE000001";
        public static string SET_ELEVATION_BTM = "055A06001220AE000002";
        public static string SET_ELEVATION_RESPONSE = "80055B05001220AE0000";
        
        public static string GET_CIRCLE_SIZE = "055A06001220B1000000";
        public static string GET_CIRCLE_SIZE_RESPONSE_PREFIX = "80055B07001220B100";
        public static string SET_CIRCLE_SIZE_RESPONSE = "80055B05001220B00000";
        public static string SET_CIRCLE_SIZE(byte front, byte middle, byte back)//0x00(0ff),0x01(inner),0x02(outter)
        {
            return "055A08001220B00000" + front.ToString("X2") + middle.ToString("X2") + back.ToString("X2");
        }

        public static string GET_TURBO_BASE = "055A06001220AD000000";
        public static string GET_TURBO_BASE_PREFIX = "80055B05001220AD00";
        public static string SET_TURBO_BASE(byte value)
        {
            return "055A06001220AC0000" + value.ToString("X2");
        }

        public static string GET_TURBO_BASE_ADAPTIVE = "055A06001220BC000000";
        public static string GET_TURBO_BASE_ADAPTIVE_PREFIX = "80055B05001220BC00";
        public static string SET_TURBO_BASE_ADAPTIVE(byte value)
        {
            return "055A06001220BB0000" + value.ToString("X2");
        }

        public static string GET_TURBO_BASE_STYLE = "055A06001220B4000000";
        public static string GET_TURBO_BASE_STYLE_PREFIX = "80055B05001220B400";
        public static string SET_TURBO_BASE_STYLE(byte value)
        {
            return "055A06001220B30000" + value.ToString("X2");
        }

        public static string GET_DYNAMIC_RANGE = "055A060012208A000000";
        public static string GET_DYNAMIC_RANGE_PREFIX = "80055B050012208A00";
        public static string SET_DYNAMIC_RANGE(byte value)
        {
            return "055A06001220890000" + value.ToString("X2");
        }

        public static string GET_MIC_RECORDING_TONE = "055A060012208E000000";
        public static string GET_MIC_RECORDING_TONE_PREFIX = "80055B050012208E00";
        public static string SET_MIC_RECORDING_TONE(byte value)
        {
            return "055A060012208D0000" + value.ToString("X2");
        }

        public static string GET_MIC_AUTO_GAIN = "055A0600122090000000";
        public static string GET_MIC_AUTO_GAIN_PREFIX = "80055B050012209000";
        public static string SET_MIC_AUTO_GAIN(byte value)
        {
            return "055A060012208F0000" + value.ToString("X2");
        }

        public static string GET_EQ_ON_OFF = "055A0600122086000000";
        public static string GET_EQ_ON_OFF_PREFIX = "80055B050012208600";
        public static string SET_EQ_ON = "055A0600122085000001";
        public static string SET_EQ_OFF = "055A0600122085000000";
        public static string SET_EQ_ON_OFF_PREFIX = "80055B050012208500";

        public static string GET_EQ_GAIN = "055A0600122084000000";
        public static string GET_EQ_GAIN_PREFIX = "80055B120012208400";
        public static string SET_EQ_GAIN(byte[] gains)
        {
            string command = "055A13001220830000";
            for (int i = 0; i < gains.Length; i++)
            {
                command += gains[i].ToString("X2");
            }
            return command;
        }

        public static string GET_PEQ_PRESET_NO = "055A060012209A0000";
        public static string GET_PEQ_PRESET_NO_PREFIX = "80055B050012209A00";
        public static string SET_PEQ_PRESET_NO(byte value)
        {
            return "055A06001220990000" + value.ToString("X2");
        }

        public static string GET_CUSTOM_PEQ_GAIN = "055A0600122092000000";
        public static string GET_CUSTOM_PEQ_GAIN_PREFIX = "80055B120012209200";
        public static string SET_CUSTOM_PEQ_GAIN(byte[] gains)
        {
            string command = "055A13001220910000";
            for (int i = 0; i < gains.Length; i++)
            {
                command += gains[i].ToString("X2");
            }
            return command;
        }

        public static string GET_TAILOR_ID_PEQ_GAIN = "055A0600122094000000";
        public static string GET_TAILOR_ID_PEQ_GAIN_PREFIX = "80055B120012209400";
        public static string SET_TAILOR_ID_PEQ_GAIN(byte[] gains)
        {
            string command = "055A13001220930000";
            for (int i = 0; i < gains.Length; i++)
            {
                command += gains[i].ToString("X2");
            }
            return command;
        }

        //Auracast related
        public static string GET_AURACAST_PRIORITY_ON_OFF = "055A0600122061000000";
        public static string SET_AURACAST_PRIORITY_ON_OFF(byte value)
        {
            return "055A06001220600000" + value.ToString("X2");
        }
        public static string GET_AURACAST_CHANNEL_NAME = "055A0600122063000000";
        public static string SET_AURACAST_CHANNEL_NAME(string channelName)
        {
            int totalLength = 6 + channelName.Length;
            int nameLength = channelName.Length;
            byte[] channelNameBytes = Encoding.UTF8.GetBytes(channelName);
            string channelNameHex = BitConverter.ToString(channelNameBytes).Replace("-", "");
            return "055A" + totalLength.ToString("X2") + "001220620000" + nameLength.ToString("X2") + channelNameHex;
        }
        public static string GET_AURACAST_PASSWORD = "055A0600122065000000";
        public static string SET_AURACAST_PASSWORD_NAME(string password)
        {
            int totalLength = 6 + password.Length;
            int passwordLength = password.Length;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            string passwordHex = BitConverter.ToString(passwordBytes).Replace("-", "");
            return "055A" + totalLength.ToString("X2") + "001220640000" + passwordLength.ToString("X2") + passwordHex;
        }
        public static string GET_AUTO_GEIN_ON_OFF = "055A0600122090000000";
        public static string SET_AUTO_GEIN_ON_OFF(byte value)
        {
            return "055A060012208F0000" + value.ToString("X2");
        }
    }
}