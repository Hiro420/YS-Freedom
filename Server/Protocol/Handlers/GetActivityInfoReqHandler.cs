
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
	public class GetActivityInfoReqHandler : IYuanShenHandler
	{
		public GetActivityInfoReqHandler() { }
		public Task HandleAsync(YuanShenPacket packet, Player player)
		{
			MsgGetActivityInfoReq req = (MsgGetActivityInfoReq)packet;

			MsgGetActivityInfoRsp rsp = new MsgGetActivityInfoRsp // depends on ActivityScheduleInfoNotify
			{
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
				packetBody = new GetActivityInfoRsp
				{
                },
            };

			var mtrialAvatarInfo = new TrialAvatarActivityDetailInfo { };
			mtrialAvatarInfo.RewardInfoList.Add(new TrialAvatarActivityRewardDetailInfo { TrialAvatarIndexId = 7,  });
			mtrialAvatarInfo.RewardInfoList.Add(new TrialAvatarActivityRewardDetailInfo { TrialAvatarIndexId = 29, });
			mtrialAvatarInfo.RewardInfoList.Add(new TrialAvatarActivityRewardDetailInfo { TrialAvatarIndexId = 18, });

			rsp.PacketBody.ActivityInfoList.Add(new ActivityInfo { ActivityId = 5002, ScheduleId = 5002010, BeginTime = 1617728400, EndTime = 1619531999, ActivityType = 4, TrialAvatarInfo = mtrialAvatarInfo , });

			Console.WriteLine("MsgGetActivityInfoRsp");
			return player.Session.SendAsync(rsp.AsBytes());
		}
	}
}

