using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using YSFreedom.Common.Util;

// A good portion of this code is heavily based on
// https://github.com/cheadaq/kcp-dotnet/blob/master/Source/Network/KCP.cs
// as well as the original ikcp sources.

/*
    Differences from standard KCP protocol:
    * Right after the conv (conversation) field (offset 0x0 in the header),
      there is new field: token. I'm not sure what it's used for, but it's 
      part of the protocol and we have to account for it.
*/

namespace YSFreedom.Common.Net
{
    public class KCP
    {
        public delegate void OutputDelegate(byte[] data);

        // As far as I know, we only need to be concerned about these two commands for now. I haven't seen any others.
        public const byte CMD_PUSH = 0x51;
        public const byte CMD_ACK = 0x52;

        // Handshake magic values
        // See GenshinPktCap/packets_kcp.py
        public static readonly uint[] HANDSHAKE_MAGIC_CONNECT = {0xFF, 0xFFFFFFFF};
        public static readonly uint[] HANDSHAKE_MAGIC_SEND_BACK_CONV = {0x145, 0x14514545};

        // Normally determined from the first handshake packet
        public uint Conversation = 0;
        public uint Token = 0;

        // Connection timeout
        public int Timeout = 20000; // 20 seconds
        public bool Connected = false;
        // Callback used to send data
        public OutputDelegate Output;

        private LinkedList<Segment> sendQ;
        private LinkedList<Segment> recvQ;
        private HashSet<uint> recvSnQ;
        private Stack<uint[]> sendAckQ;

        private int LastAckTime = 0;

        // Packet serial number
        private uint sendSn = 0;
        // MTU/MSS
        private uint mtu = 1400;
        private uint mss { get { return mtu - 64; } }


        public KCP()
        {
            sendQ = new LinkedList<Segment>();
            recvQ = new LinkedList<Segment>();
            recvSnQ = new HashSet<uint>();
            sendAckQ = new Stack<uint[]>();

            sendSn = 1; // TODO: random?
        }

        public struct kcp_packet
        {
            public UInt32 conv;
            public UInt32 yuanshen; // yuanshen magic
            public byte cmd; // command
            public byte frg;
            public UInt16 wnd; // window
            public UInt32 ts; // timestamp
            public UInt32 sn; // packet serial number
            public UInt32 una;
            public UInt32 len; // len of data??
            public byte[] data; // optional
        }

        public void Send(byte[] data, int offset, int len)
        {
            if (!Connected) throw new Exception("Not connected.");

            if (len < mss)
            {
                Segment seg = new Segment() {
                    data = data[offset..(offset+len)].ToArray(), // We make a copy because the array can't be trusted to remain the same
                };
                sendQ.AddLast(seg);
            }

            if ((len / mss) > 254)
                throw new OverflowException("Packet too large. Cannot fragment.");

            // TODO: fragmentation
        }

        public byte[] Receive(bool peek = false)
        {
            int peeksz = PeekSize();
            if (!Connected && peeksz < 0) throw new Exception("Not connected.");

            if (peeksz < 0)
                return null;

            var ret = new byte[peeksz];
            var offset = 0;

            LinkedListNode<Segment> next = null;
            for (var node = recvQ.First; node != null; node = next) 
            {
                var seg = node.Value;
                next = node.Next;

                seg.data.CopyTo(ret, offset);
                offset += seg.data.Length;

                if (!peek)
                    recvQ.Remove(node);

                if (seg.frg == 0)
                    break;
            }

            return ret;
        }

        // Get size of the next packet, otherwise
        // return a negative value if there's no packet
        public int PeekSize()
        {
            // We've received no segments. There's no available packet.
            if (recvQ.Count == 0)
                return -1;

            var node = recvQ.First;
            var seg = node.Value;

            // Not a fragmented packet. Just return segment length.
            if (seg.frg == 0)
                return seg.data.Length;

            // We haven't received all fragments of the packet.
            // Therefore there's no available packet right now.
            if (recvQ.Count < seg.frg + 1)
                return -1;

            // Only used for fragmented packets.
            // frg = 0 signals that there are no more fragmented packets.
            int len = 0;
            for (; node != null; node = node.Next) {
                seg = node.Value;
                len += seg.data.Length;
                if (seg.frg == 0) break;
            }

            return len;
        }

        public void Input(byte[] data)
        {
            int offset = 0;
            while (true)
            {
                var seg = new Segment(data[offset..^0]);
                MarkAck(seg.sn);

                switch (seg.cmd)
                {
                    case CMD_PUSH:
                        sendAckQ.Push(new uint[] {seg.sn, seg.ts});
                        if (recvSnQ.Contains(seg.sn))
                            break;

                        recvSnQ.Add(seg.sn);
                        InsertSegment(recvQ, seg);
                    break;
                }
            }
        }

        private void InsertSegment(LinkedList<Segment> queue, Segment seg)
        {
            LinkedListNode<Segment> node = null, prev = null;
            for (node = queue.Last; node != null; node = prev)
            {
                var seg2 = node.Value;
                prev = node.Previous;
                if ((seg.sn - seg2.sn) > 0) break;
            }

            if (node != null)
                queue.AddAfter(node, seg);
            else
                queue.AddFirst(seg);
        }

        private void MarkAck(uint una)
        {
            LinkedListNode<Segment> next = null;
            for (var node = sendQ.First; node != null; node = next)
            {
                var seg = node.Value;
                next = node.Next;

                if ((una - seg.sn) > 0)
                {
                    sendQ.Remove(node);
                } else break;
            }
        }

        public void Flush()
        {
            while (sendAckQ.Count > 0) {
                var item = sendAckQ.Pop();

                var seg = new Segment() {
                    cmd = CMD_ACK,
                    una = NextSn(),
                    sn = item[0],
                    ts = item[1],
                };
            }
            
            for (var node = sendQ.First; node != null; node = node.Next)
            {
                var seg = node.Value;
                seg.conv = Conversation;
                seg.token = Token;
                if (seg.sn == 0)
                    seg.sn = NextSn();
                if (seg.ts == 0)
                    seg.ts = 0;

                seg.sent = true;
                Output(seg.AsBytes());

                sendQ.Remove(node);
            }
        }

        private uint NextSn()
        {
            return sendSn++;
        }

        internal class Segment
        {
            public const int HEADER_LEN = 28;

            public uint conv = 0;
            public uint token = 0;
            public byte cmd = 0;
            public byte frg = 0;
            public ushort wnd = 0;
            public uint ts = 0;
            public uint sn = 0;
            public uint una = 0;
            public uint len = 0;
            public byte[] data = null;

            // Not part of the header or packet, but allows us to keep track of whether or not a packet was truly sent.
            public bool sent = false;

            public Segment() {}
            public Segment(byte[] buffer)
            {
                Decode(buffer, true, true);
            }

            public void Encode(byte[] buffer)
            {
                buffer.SetUInt32(0, conv);
                buffer.SetUInt32(4, token);
                buffer[8] = cmd;
                buffer[9] = frg;
                buffer.SetUInt16(10, wnd);
                buffer.SetUInt32(12, ts);
                buffer.SetUInt32(16, sn);
                buffer.SetUInt32(20, una);
                buffer.SetUInt32(24, len);
            }

            public void Decode(byte[] buffer, bool setData = true, bool copyData = false)
            {
                if (buffer.Length < HEADER_LEN) throw new Exception("Buffer to decode too small.");

                conv = buffer.GetUInt32(0);
                token = buffer.GetUInt32(0);
                cmd = buffer[8];
                frg = buffer[9];
                wnd = buffer.GetUInt16(10);
                ts = buffer.GetUInt32(12);
                sn = buffer.GetUInt32(16);
                una = buffer.GetUInt32(20);
                len = buffer.GetUInt32(24);

                if (setData)
                {
                    data = buffer[28..(28+(int)len)];
                    if (copyData) data = data.ToArray();
                }
                
            }

            public byte[] AsBytes(bool includeData = true)
            {
                byte[] ret = new byte[HEADER_LEN + (includeData && data != null ? data.Length : 0)];
                Encode(ret);
                if (includeData)
                    data.CopyTo(ret, 28);
                return ret;
            }
        }

        internal class Handshake {
            public const int HANDSHAKE_LEN = 20;

            uint magic1 = 0;
            uint conv = 0;
            uint token = 0;
            uint data = 0;
            uint magic2 = 0;

            void Encode(byte[] buffer)
            {
                buffer.SetUInt32(0, magic1);
                buffer.SetUInt32(4, conv);
                buffer.SetUInt32(8, token);
                buffer.SetUInt32(12, data);
                buffer.SetUInt32(16, magic2);
            }

            void Decode(byte[] buffer)
            {
                magic1 = buffer.GetUInt32(0);
                conv = buffer.GetUInt32(4);
                token = buffer.GetUInt32(8);
                data = buffer.GetUInt32(12);
                magic2 = buffer.GetUInt32(16);
            }

            bool VerifyMagic(uint[] magic)
            {
                return magic1 == magic[0] && magic2 == magic[1];
            }
        }

    }
}

