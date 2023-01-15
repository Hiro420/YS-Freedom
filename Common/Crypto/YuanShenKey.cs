using System;
using System.Linq;
using System.Net;
using System.Text;

// This class contains an implementation of miHoYo's awful XOR & mt64 based
// cryptography routines. Truly awful. I wonder if the developers suffered from terminal stupidity.

namespace YSFreedom.Common.Crypto
{
    public class YuanShenKey
    {
        public static readonly YuanShenKey NoOp = new YuanShenKey(new byte[4096]);
        public const int LEN = 4096;
        public byte[] Bytes;

        public YuanShenKey()
        {
            Bytes = new byte[LEN];
        }

        public YuanShenKey(byte[] buffer)
        {
            if (buffer.Length != LEN)
                throw new ArgumentException("Key must be 4096 bytes", "buffer");
            Bytes = buffer.ToArray();
        }

        public YuanShenKey(ulong seed)
        {
            Bytes = new byte[LEN];
            MersenneKeyGen(Bytes, seed);
        }

        public void Crypt(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] ^= Bytes[i % LEN];
        }

        public static YuanShenKey FromBase64(string text)
        {
            return new YuanShenKey(Convert.FromBase64String(text));
        }

        public static void MersenneKeyGen(byte[] buf, ulong seed)
        {

            ulong[] state = new ulong[624];
            ulong v7 = 1, v9 = 0x137, v3;
            state[0] = seed;

            ulong[] pv1 = state;
            int pv1idx = 0;
            do
            {
                pv1idx++;
                seed = (seed ^ seed >> 0x3e) * 0x5851f42d4c957f2d + v7;
                v7++;
                pv1[pv1idx] = seed;
                v9--;
            } while (v9 != 0);

            int v5 = 0;
            int pv6idx = 2;
            do
            {
                int v8 = (v5 + 0x138) % 0x270;
                if (v8 == 0x138)
                {
                    v7 = 0x138;
                    pv1idx = 0x9c;
                    do
                    {
                        v3 = (ulong)(((uint)state[pv1idx - 0x9b] ^ (uint)state[pv1idx - 0x9c]) & 0x7fffffff) ^ state[pv1idx - 0x9c];
                        state[pv1idx + 0x9c] = (ulong)(int)-((uint)v3 & 1) & 0xb5026f5aa96619e9 ^ v3 >> 1 ^ state[pv1idx];
                        v7--;
                        pv1idx++;
                    } while (v7 != 0);
                }
                else if (v8 == 0)
                {
                    v7 = 0x9c;
                    pv1idx = 0x1d4;
                    do
                    {
                        v3 = (ulong)(((uint)state[pv1idx - 0x9b] ^ (uint)state[pv1idx - 0x9c]) & 0x7fffffff) ^ state[pv1idx - 0x9c];
                        state[pv1idx - 0x1d4] = (ulong)(int)-((uint)v3 & 1) & 0xb5026f5aa96619e9 ^ v3 >> 1 ^ state[pv1idx];
                        v7--;
                        pv1idx++;
                    } while (v7 != 0);

                    v7 = 0x9b;
                    pv1idx = 0;
                    do
                    {
                        v3 = (ulong)(((uint)state[pv1idx + 0x1d5] ^ (uint)state[pv1idx + 0x1d4]) & 0x7fffffff) ^ state[pv1idx + 0x1d4];
                        state[pv1idx + 0x9c] = (ulong)(int)-((uint)v3 & 1) & 0xb5026f5aa96619e9 ^ v3 >> 1 ^ state[pv1idx];
                        v7--;
                        pv1idx++;
                    } while (v7 != 0);
                }

                v5++;
                v3 = state[v8] ^ state[v8] >> 0x1d & 0x555555555;
                v3 = v3 ^ (v3 & 0x38eb3ffff6d3) << 0x11;
                ulong v2 = v3 ^ (v3 & 0xffffffffffffbf77) << 0x25;
                ulong v4 = v2 >> 0x2b ^ v2;
                buf[pv6idx + 5] = (byte)v4;
                buf[pv6idx - 2] = (byte)(v2 >> 0x38);
                buf[pv6idx - 1] = (byte)(v2 >> 0x30);
                buf[pv6idx + 0] = (byte)(v2 >> 0x28);
                buf[pv6idx + 1] = (byte)(v2 >> 0x20);
                buf[pv6idx + 2] = (byte)(v3 >> 0x18);
                buf[pv6idx + 3] = (byte)(v4 >> 0x10);
                buf[pv6idx + 4] = (byte)(v4 >> 0x08);
                pv6idx = pv6idx + 8;
            } while (v5 < 0x200);

        }
    }
}
