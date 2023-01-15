using System;
using System.Net;
using System.Net.Sockets;

namespace YSFreedom.Common.UDP
{
    public static class UDPHandler
    {
        public struct udp_packet
        {
            public int len;
            public byte[] data;
        }

        /**
         * send_udp
         * !!this is a blocking function!!
         * return: len of bytes writen to dst
         **/
        public static int send_udp(byte[] data, int len, int port, string dst = "127.0.0.1")
        {
            UdpClient udpClient = new UdpClient();

            try
            {
                udpClient.Connect(dst, port);
                udpClient.Send(data, data.Length);
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                len = 0;
            } finally
            {
                udpClient.Close();
            }
            return len;
        }

        /**
         * recv_udp
         * !!this is a blocking function!!
         * return: len of bytes writen to dst
         **/
        public static udp_packet recv_udp(int port, int len = 65535)
        {
            udp_packet packet = new udp_packet();
            UdpClient udpClient = new UdpClient(port);
            byte[] recv_bytes;
            try
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, port);
                recv_bytes = udpClient.Receive(ref RemoteIpEndPoint);
                packet.len = recv_bytes.Length;
                packet.data = recv_bytes;
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            } finally
            {
                udpClient.Close();
            }

            return packet;
        }
    }
}
