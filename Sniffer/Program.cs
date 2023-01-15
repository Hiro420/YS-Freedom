using System;
using System.IO;
using System.Threading;
using PacketDotNet;
using SharpPcap;
using YSFreedom.Common.Util;
using YSFreedom.Common.Protocol;
using YSFreedom.Common.Crypto;
using System.Linq;
using YSFreedom.Common.Native;

namespace YuanShen_Sniffer
{
    class Program
    {

        static void Main(string[] args)
        {
            byte[] avatarprotosaved = File.ReadAllBytes("AvatarDataNotify.bin");
            AvatarDataNotify mavatar = AvatarDataNotify.Parser.ParseFrom(avatarprotosaved);
            Console.WriteLine(mavatar.ToString());
        }
    }
}