
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSFreedom.Common.Protocol;
using YSFreedom.Common.Protocol.Messages;
using YSFreedom.Server.Game;

namespace YSFreedom.Server.Protocol.Handlers
{
	public class EnterSceneDoneReqHandler : IYuanShenHandler
	{
		public EnterSceneDoneReqHandler() { }
		public async Task HandleAsync(YuanShenPacket packet, Player player)
		{
			MsgEnterSceneDoneReq req = (MsgEnterSceneDoneReq)packet;

			MsgEnterSceneDoneRsp rsp = new MsgEnterSceneDoneRsp
			{
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
				packetBody = new EnterSceneDoneRsp
				{
					EnterSceneToken = 12383, // same as in PlayerEnterSceneNotify
				},
            };

			MsgSceneEntityAppearNotify msgSceneEntityAppearNotify = new MsgSceneEntityAppearNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = SceneEntityAppearNotify.Parser.ParseFrom(File.ReadAllBytes("SceneEntityAppearNotify.bin")),
			};

			await player.Session.SendAsync(msgSceneEntityAppearNotify.AsBytes());
			Console.WriteLine("msgSceneEntityAppearNotify");
			Console.WriteLine(msgSceneEntityAppearNotify.PacketBody.ToString());

			await player.Session.SendAsync(rsp.AsBytes());
			Console.WriteLine("MsgEnterSceneReadyRsp");
			Console.WriteLine(rsp.PacketBody.ToString());
			return;
		}
	}
}

