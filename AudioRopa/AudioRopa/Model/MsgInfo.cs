using System;
namespace AudioRopa.Model
{
    public class MsgInfo
    {
        public int statusCode;
        public int msgID;
        public double extra1;
        public double extra2;
        public byte[] extra3;
        public byte[] data3_raw;

        public override string ToString()
        {
            return $"statusCode={statusCode}, msgID={msgID}, extra1={extra1}, extra2={extra2}, extra3={BitConverter.ToString(extra3 ?? new byte[0])}";
        }
    }
}

