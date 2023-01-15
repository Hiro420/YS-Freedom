
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
	public class PostEnterSceneReqHandler : IYuanShenHandler
	{
		public PostEnterSceneReqHandler() { }
		public async Task HandleAsync(YuanShenPacket packet, Player player)
		{
			MsgPostEnterSceneReq req = (MsgPostEnterSceneReq)packet;

			MsgPostEnterSceneRsp rsp = new MsgPostEnterSceneRsp
			{
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
				packetBody = new PostEnterSceneRsp
				{
					EnterSceneToken = 12383, // same as in PlayerEnterSceneNotify
				},
            };



			await player.Session.SendAsync(rsp.AsBytes());
			Console.WriteLine("MsgPostEnterSceneRsp");
			Console.WriteLine(rsp.PacketBody.ToString());
			return;
		}
	}
}

