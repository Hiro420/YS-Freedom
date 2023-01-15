using System;
using System.Linq;
using YSFreedom.Common.Util;
using YSFreedom.Common.Crypto;
using Google.Protobuf;
using Newtonsoft.Json.Converters;
using System.IO;
using Newtonsoft.Json;

namespace YSFreedom.Common.Protocol
{
    public class YuanShenPacket
    {
        public const int HEADER_LEN = 8;
        public const int MAGIC_LEN = 2;
        public static readonly ushort[] MAGIC = { 0x4567, 0x89AB };

        public EMsgType Type { get { return type; } }

        public EMsgType type;

        public PacketHead metaData;
        private ushort metaDataLen = 0xFFFF;

        public byte[] data; // The only purpose of this field now is to parse unknown protobuf messages;
        private uint dataLen = 0xFFFFFFFF; // The only purpose of this field now is to parse unknown protobuf messages;

        public YuanShenPacket(EMsgType msgType = EMsgType.Invalid) { type = msgType; }
        public YuanShenPacket(byte[] incomingData, bool noCopy = false) { Decode(noCopy ? incomingData : incomingData.ToArray()); }

        public int Length
        {
            get
            {
                return HEADER_LEN + MAGIC_LEN * 2 +
                (metaData == null ? 0 : metaData.CalculateSize())  +
                GetDataLen();
            }
        }
        public virtual int GetDataLen()
        {
            return data == null ? 0 : data.Length;
        }

        public void Encode(byte[] buffer, bool setFinalMagic = true)
        {
            buffer.SetUInt16(0, MAGIC[0], true);
            buffer.SetUInt16(2, (UInt16)type, true);
            buffer.SetUInt16(4, metaDataLen != 0xFFFF ? metaDataLen : (ushort)metaData.CalculateSize(), true);
            buffer.SetUInt32(6, dataLen != 0xFFFFFFFF ? dataLen : (uint)GetDataLen(), true);

            if (setFinalMagic)
                buffer.SetUInt16(buffer.Length - 2, MAGIC[1], true);
        }
        protected virtual void EncodeData(ref byte[] buffer, Int32 offset)
        {
            data.CopyTo(buffer, offset);
        }

        private void Decode(byte[] buffer)
        {
            if (buffer.Length < 4)
                throw new ArgumentException("Packet is too small to be valid", "buffer");

            if (buffer.GetUInt16(0, true) != MAGIC[0] || buffer.GetUInt16(buffer.Length - 2, true) != MAGIC[1])
                throw new ArgumentException("Invalid packet magic", "buffer");

            type = (EMsgType)buffer.GetUInt16(2, true);

            metaDataLen = buffer.GetUInt16(4, true);
            dataLen = buffer.GetUInt32(6, true);

            // If there's more than one packet on a message
            // Should consider using a RecyclableMemoryStream instead of byte[] for tracking position
            if (buffer.Length != MAGIC_LEN * 2 + HEADER_LEN + metaDataLen + dataLen)
                throw new ArgumentException("Packet is invalid or buffer contains more than one packet", "buffer");

            int offset = MAGIC_LEN + HEADER_LEN;
            metaData = PacketHead.Parser.ParseFrom(buffer, offset, metaDataLen);

            offset += metaDataLen;

            DecodeData(buffer, offset, dataLen);
        }
        protected virtual void DecodeData(byte[] buffer, Int32 offset, UInt32 metaDataLen)
        {
            data = buffer[offset..(offset + (int)dataLen)];
        }

        public virtual byte[] AsBytes(bool includeData = true)
        {
            var ret = new byte[includeData ? Length : (HEADER_LEN + MAGIC_LEN)];
            Encode(ret, includeData);
            if (includeData) {
                if (metaData != null) metaData.ToByteArray().CopyTo(ret, MAGIC_LEN + HEADER_LEN);
                if (data != null) EncodeData(ref ret, MAGIC_LEN + HEADER_LEN + (metaData != null ? metaData.CalculateSize() : 0));
            }
            return ret;
        }

        public override string ToString()
        {
            // We are using JsonConvert to sacrifice performance for a better idented Debug Output
            var _metaData = JsonConvert.DeserializeObject(metaData.ToString());

            return $"msgType : {type.ToString()}\nHead :\n{JsonConvert.SerializeObject(_metaData, Formatting.Indented)}\nBody :\n{DumpBody()}";
        }

        protected virtual String DumpBody() { return ""; }
    }
}
