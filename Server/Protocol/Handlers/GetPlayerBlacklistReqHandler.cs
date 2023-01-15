
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSFreedom.Common.Protocol;
using YSFreedom.Common.Protocol.Messages;
using YSFreedom.Server.Game;

namespace YSFreedom.Server.Protocol.Handlers
{
	public class GetPlayerBlacklistReqHandler : IYuanShenHandler
	{
		public GetPlayerBlacklistReqHandler() { }
		public Task HandleAsync(YuanShenPacket packet, Player player)
		{
			MsgGetPlayerBlacklistReq req = (MsgGetPlayerBlacklistReq)packet;

			MsgGetPlayerBlacklistRsp rsp = new MsgGetPlayerBlacklistRsp
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
				packetBody = new GetPlayerBlacklistRsp
				{
					Retcode = 0,
				}
            };

			return player.Session.SendAsync(rsp.AsBytes());
		}
	}
}

