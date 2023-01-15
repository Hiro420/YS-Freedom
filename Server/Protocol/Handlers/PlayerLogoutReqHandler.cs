
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
	public class PlayerLogoutReqHandler : IYuanShenHandler
	{
		public PlayerLogoutReqHandler() { }
		public Task HandleAsync(YuanShenPacket packet, Player player)
		{
			MsgPlayerLogoutReq req = (MsgPlayerLogoutReq)packet;

			MsgPlayerLogoutRsp rsp = new MsgPlayerLogoutRsp
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                }
            };

			throw new NotImplementedException();
		}
	}
}

