
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
	public class EnterSceneReadyReqHandler : IYuanShenHandler
	{
		public EnterSceneReadyReqHandler() { }
		public async Task HandleAsync(YuanShenPacket packet, Player player)
		{
			MsgEnterSceneReadyReq req = (MsgEnterSceneReadyReq)packet;

			MsgEnterSceneReadyRsp rsp = new MsgEnterSceneReadyRsp
			{
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
				packetBody = new EnterSceneReadyRsp
				{
					EnterSceneToken = 12383, // same as in PlayerEnterSceneNotify
				},
            };

			MsgEnterScenePeerNotify msgEnterScenePeerNotify = new MsgEnterScenePeerNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = new EnterScenePeerNotify
				{
					EnterSceneToken = 12383, // same as in PlayerEnterSceneNotify
					DestSceneId = 3,
					PeerId = 1,
					HostPeerId = 1,
				},
			};

			await player.Session.SendAsync(msgEnterScenePeerNotify.AsBytes());
			Console.WriteLine("msgEnterScenePeerNotify");
			Console.WriteLine(msgEnterScenePeerNotify.PacketBody.ToString());

			await player.Session.SendAsync(rsp.AsBytes());
			Console.WriteLine("MsgEnterSceneReadyRsp");
			Console.WriteLine(rsp.PacketBody.ToString());
			return;
		}
	}
}

