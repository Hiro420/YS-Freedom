using System;
using System.Collections.Generic;
using System.Text;
using YSFreedom.Common.Util;

namespace ProtocolInspector
{
    internal class Handshake
    {
        public static readonly uint[] MAGIC_CONNECT = { 0xFF, 0xFFFFFFFF };
        public static readonly uint[] MAGIC_SEND_BACK_CONV = { 0x145, 0x14514545 };
        public static readonly uint[] MAGIC_DISCONNECT = { 0x194, 0x19419494 };
        public const int LEN = 20;

        public uint Magic1;
        public uint Conv;
        public uint Token;
        public uint Data;
        public uint Magic2;

        public Handshake() { }
        public Handshake(uint[] magic, uint conv = 0, uint token = 0, uint data = 0)
        {
            Magic1 = magic[0];
            Conv = conv;
            Token = token;
            Data = data;
            Magic2 = magic[1];
        }

        public void Encode(byte[] buffer)
        {
            buffer.SetUInt32(0, Magic1, true);
            buffer.SetUInt32(4, Conv, true);
            buffer.SetUInt32(8, Token, true);
            buffer.SetUInt32(12, Data, true);
            buffer.SetUInt32(16, Magic2, true);
        }

        public void Decode(byte[] buffer, uint[] verifyMagic = null)
        {
            if (buffer.Length < LEN)
                throw new ArgumentException("Handshake packet too small", "buffer");

            Magic1 = buffer.GetUInt32(0, true);
            Conv = buffer.GetUInt32(4, true);
            Token = buffer.GetUInt32(8, true);
            Data = buffer.GetUInt32(12, true);
            Magic2 = buffer.GetUInt32(16, true);

            if (verifyMagic != null)
            {
                if (Magic1 != verifyMagic[0] || Magic2 != verifyMagic[1])
                    throw new ArgumentException("Invalid handshake packet", "buffer");
            }
        }

        public byte[] AsBytes()
        {
            var ret = new byte[20];
            Encode(ret);
            return ret;
        }
    }
}
