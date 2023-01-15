using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSFreedom.Common.Protocol;
using YSFreedom.Common.Protocol.Messages;
using YSFreedom.Server.Game;
using Google.Protobuf;

namespace YSFreedom.Server.Protocol.Handlers
{
    public class PingReqHandler : IYuanShenHandler
    {
        public PingReqHandler() { }
        public Task HandleAsync(YuanShenPacket packet, Player player)
        {
            MsgPingReq req = (MsgPingReq)packet;

            MsgPingRsp rsp = new MsgPingRsp
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
                packetBody = new PingRsp
                {
                    Retcode = 0,
                    Seq = req.PacketBody.Seq,
                    ClientTime = req.PacketBody.ClientTime,
                }
            };

            return player.Session.SendAsync(rsp.AsBytes());
        }
    }
}

