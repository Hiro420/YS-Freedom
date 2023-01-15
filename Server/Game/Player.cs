using System;
using System.Threading.Tasks;
using YSFreedom.Server.Auth;
using YSFreedom.Common.Net;
using YSFreedom.Common.Protocol;
using YSFreedom.Server.Protocol;
using YSFreedom.Common.Protocol.Messages;
using YSFreedom.Server.Game;
using Serilog;

namespace YSFreedom.Server.Game
{
    public class Player
    {
        public ulong UID { get { return Account != null ? Account.UID : 0; } }

        public IAuthCalls AuthCalls;
        public MHYAccount Account;
        public KCP RawConn;
        public YuanShenSession Session;

        public Player(KCP conn)
        {
            RawConn = conn;
            Account = null;
            Session = new YuanShenSession(conn);
        }

        public async Task HandlePacketAsync(byte[] packet)
        {
            // I feel like doing this is terrible, but there seriously is no alternative
            EMsgType pktType = (new YuanShenPacket(packet, true)).Type;

            Log.Debug("Packet received, type: {pktType}.", pktType);

            var handler = YuanShenHandlerFactory.NewInstance(pktType);
            if (handler != null)
            {
                var decodedPkt = YuanShenMessageFactory.NewInstance(pktType, packet);
                //Log.Debug("Packet data: {@pktData}", decodedPkt);

                await handler.HandleAsync(decodedPkt, this);
            }
            RawConn.Flush();
        }
    }
}
