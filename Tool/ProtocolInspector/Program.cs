using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.IO;
using YSFreedom.Common.Crypto;
using YSFreedom.Common.Protocol;
using YSFreedom.Common.Protocol.Messages;
using YSFreedom.Common.Util;
using static YSFreedom.Common.Net.KCP;

namespace ProtocolInspector
{
    class Program
    {
        static ConnectionState State;
        static Handshake handshake;
        static YuanShenKey key;

        static KcpClient yuanShenClient, yuanShenServer;

        /// ALERT, Proced with caution, Ugly code - But it works :)
        /// Run and pass as 1st argument a WireShark capture file, use this filter (udp portrange 22100-22102)
        static void Main(string[] args)
        {
            Console.WriteLine($"YSFreedom - Protocol Inspector using SharpPcap {SharpPcap.Version.VersionString}");

            ICaptureDevice device;

            if(!File.Exists(args[0])) { Console.WriteLine("CaptureFile Not Found"); return; }

            try
            {
                Console.WriteLine($"Opening '{args[0]}");

                // Get an offline device
                device = new CaptureFileReaderDevice(args[0]);

                // Open the device
                device.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception when opening file" + e.ToString());
                return;
            }

            YuanShenMessageFactory.InitializeFactory();

            // Register our handler function to the 'packet arrival' event
            device.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival);

            State = ConnectionState.DISCONNECTED;

            device.Capture();
            device.Close();

            Console.Write("Hit 'Enter' to exit...");
            Console.ReadLine();
        }
        private static object locker = new object();
        private static int packetIndex = 0;
        private static void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            if (e.Packet.LinkLayerType == LinkLayers.Ethernet)
            {
                var ethernetPacket = Packet.ParsePacket(LinkLayers.Ethernet, e.Packet.Data).Extract<EthernetPacket>();
                var ipPacket = Packet.ParsePacket(LinkLayers.Ethernet, e.Packet.Data).Extract<IPv4Packet>();
                var udpPacket = ipPacket.Extract<UdpPacket>();

                lock(locker)
                {
                    try
                    {
                        bool server = udpPacket.SourcePort >= 22100 && udpPacket.SourcePort <= 22102 ? true : false;
                        PreProcessPacket(udpPacket.PayloadData, server);
                        packetIndex++;
                    }
                    catch(Exception ex) { Console.WriteLine(ex.ToString()); }
                }
            }
        }

        private static void PreProcessPacket(byte[] payloadData, bool fromServer)
        {
            handshake = new Handshake();
            byte[] ysClientPacket, ysServerPacket;

            if (State == ConnectionState.HANDSHAKE_CONNECT)
            {
                UInt32 Conv = payloadData.GetUInt32(0);
                UInt32 Token = payloadData.GetUInt32(4);

                yuanShenClient = new KcpClient(Conv, Token);
                yuanShenServer = new KcpClient(Conv, Token);                    
            }

            if (State == ConnectionState.CONNECTED || State == ConnectionState.HANDSHAKE_CONNECT)
            {
                if (fromServer) yuanShenServer.Input(payloadData);
                else yuanShenClient.Input(payloadData);

                ysClientPacket = yuanShenClient.Get();
                ysServerPacket = yuanShenServer.Get();

                if (ysClientPacket != null)
                    ProcessPacket(ysClientPacket, false);

                if (ysServerPacket != null)
                    ProcessPacket(ysServerPacket, true);
            }
            else
                ProcessPacket(payloadData, fromServer);
        }

        private static void ProcessPacket(byte[] payloadData, bool fromServer)
        {
            handshake = new Handshake();

            switch (State)
            {
                case ConnectionState.DISCONNECTED:
                    try
                    {
                        handshake.Decode(payloadData, Handshake.MAGIC_CONNECT);
                        State = ConnectionState.HANDSHAKE_WAIT;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    break;
                case ConnectionState.HANDSHAKE_WAIT:
                    try
                    {
                        handshake.Decode(payloadData, Handshake.MAGIC_SEND_BACK_CONV);
                        State = ConnectionState.HANDSHAKE_CONNECT;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    break;
                case ConnectionState.CONNECTED:
                case ConnectionState.HANDSHAKE_CONNECT:

                    if (payloadData.Length <= 4)
                        return;

                    if (key == null)
                    {
                        try
                        {
                            key = KeyRecovery.FindKey(payloadData);
                            if (key == null) throw new ArgumentException("key");
                        }
                        catch { return; }
                    }

                    key.Crypt(payloadData);

                    var type = (EMsgType)payloadData.GetUInt16(2, true);
                    YuanShenPacket yuanShenPacket = YuanShenMessageFactory.NewInstance(type, payloadData);

                    if (yuanShenPacket.GetType() == typeof(MsgGetPlayerTokenRsp))
                    {
                        key = new YuanShenKey(((MsgGetPlayerTokenRsp)yuanShenPacket).PacketBody.SecretKeySeed);
                    }

                    // Note, Onyl Server Rsp from Client Req have metaData, Server Notifications doesn't
                    Console.WriteLine("----------------------------------------------------------------------");
                    String origin = fromServer ? "Server" : "Client";
                    Console.WriteLine($"PacketDump from {origin} : {yuanShenPacket.ToString()}");
                    Console.WriteLine("----------------------------------------------------------------------");

                    State = ConnectionState.CONNECTED;
                    break;
                default:
                    break;
            }
        }

    }
}
