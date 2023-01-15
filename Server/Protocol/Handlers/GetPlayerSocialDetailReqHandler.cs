
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
	public class GetPlayerSocialDetailReqHandler : IYuanShenHandler
	{
		public GetPlayerSocialDetailReqHandler() { }
		public Task HandleAsync(YuanShenPacket packet, Player player)
		{
			MsgGetPlayerSocialDetailReq req = (MsgGetPlayerSocialDetailReq)packet;

			MsgGetPlayerSocialDetailRsp rsp = new MsgGetPlayerSocialDetailRsp
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
				packetBody = new GetPlayerSocialDetailRsp
				{
					DetailData = new SocialDetail
					{
						Uid = 708845657,
						Nickname = "Fujiwara",
						Level = 7,
						AvatarId = 10000007,
						OnlineState = FriendOnlineState.FriendOnline,
						IsFriend = true,
						IsMpModeAvailable = true,
						NameCardId = 210001,
						FinishAchievementNum = 4,
					}
				}
            };

			return player.Session.SendAsync(rsp.AsBytes());
		}
	}
}

