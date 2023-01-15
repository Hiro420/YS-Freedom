
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
	public class SceneInitFinishReqHandler : IYuanShenHandler
	{
		public SceneInitFinishReqHandler() { }
		public async Task HandleAsync(YuanShenPacket packet, Player player)
		{
			MsgSceneInitFinishReq req = (MsgSceneInitFinishReq)packet;

			MsgSceneInitFinishRsp rsp = new MsgSceneInitFinishRsp
			{
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
				packetBody = new SceneInitFinishRsp
				{
					EnterSceneToken = 12383, // same as in PlayerEnterSceneNotify
				},
            };
			/**
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
			}; **/

			MsgWorldPlayerInfoNotify msgWorldPlayerInfoNotify = new MsgWorldPlayerInfoNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = WorldPlayerInfoNotify.Parser.ParseFrom(File.ReadAllBytes("WorldPlayerInfoNotify.bin")),
			};


			await player.Session.SendAsync(msgWorldPlayerInfoNotify.AsBytes());
			Console.WriteLine("msgWorldPlayerInfoNotify");
			Console.WriteLine(msgWorldPlayerInfoNotify.PacketBody.ToString());

			MsgWorldDataNotify msgWorldDataNotify = new MsgWorldDataNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = new WorldDataNotify
				{
				},

			};
			var mworldPropMap = new Dictionary<uint, PropValue>();
			mworldPropMap.Add(1, new PropValue { Ival = 0, Type = 1 });
			mworldPropMap.Add(2, new PropValue { Ival = 0, Type = 2 });

			msgWorldDataNotify.PacketBody.WorldPropMap.Add(mworldPropMap);

			await player.Session.SendAsync(msgWorldDataNotify.AsBytes());
			Console.WriteLine("msgWorldDataNotify");
			Console.WriteLine(msgWorldDataNotify.PacketBody.ToString());

			MsgTeamResonanceChangeNotify msgTeamResonanceChangeNotify = new MsgTeamResonanceChangeNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = TeamResonanceChangeNotify.Parser.ParseFrom(File.ReadAllBytes("TeamResonanceChangeNotify.bin")),
			};

			await player.Session.SendAsync(msgTeamResonanceChangeNotify.AsBytes());
			Console.WriteLine("msgTeamResonanceChangeNotify");
			Console.WriteLine(msgTeamResonanceChangeNotify.PacketBody.ToString());

			MsgAvatarEquipChangeNotify msgAvatarEquipChangeNotify = new MsgAvatarEquipChangeNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = AvatarEquipChangeNotify.Parser.ParseFrom(File.ReadAllBytes("AvatarEquipChangeNotify.bin")),
			};

			await player.Session.SendAsync(msgAvatarEquipChangeNotify.AsBytes());
			Console.WriteLine("msgAvatarEquipChangeNotify");
			Console.WriteLine(msgAvatarEquipChangeNotify.PacketBody.ToString());

			msgAvatarEquipChangeNotify = new MsgAvatarEquipChangeNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = AvatarEquipChangeNotify.Parser.ParseFrom(File.ReadAllBytes("AvatarEquipChangeNotify1.bin")),
			};

			await player.Session.SendAsync(msgAvatarEquipChangeNotify.AsBytes());
			Console.WriteLine("msgAvatarEquipChangeNotify");
			Console.WriteLine(msgAvatarEquipChangeNotify.PacketBody.ToString());

			msgAvatarEquipChangeNotify = new MsgAvatarEquipChangeNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = AvatarEquipChangeNotify.Parser.ParseFrom(File.ReadAllBytes("AvatarEquipChangeNotify2.bin")),
			};

			await player.Session.SendAsync(msgAvatarEquipChangeNotify.AsBytes());
			Console.WriteLine("msgAvatarEquipChangeNotify");
			Console.WriteLine(msgAvatarEquipChangeNotify.PacketBody.ToString());

			msgAvatarEquipChangeNotify = new MsgAvatarEquipChangeNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = AvatarEquipChangeNotify.Parser.ParseFrom(File.ReadAllBytes("AvatarEquipChangeNotify3.bin")),
			};

			await player.Session.SendAsync(msgAvatarEquipChangeNotify.AsBytes());
			Console.WriteLine("msgAvatarEquipChangeNotify");
			Console.WriteLine(msgAvatarEquipChangeNotify.PacketBody.ToString());

			MsgHostPlayerNotify msgHostPlayerNotify = new MsgHostPlayerNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = new HostPlayerNotify
				{
					HostPeerId = 1,
					HostUid = 708845657,
				},
			};

			await player.Session.SendAsync(msgHostPlayerNotify.AsBytes());
			Console.WriteLine("msgHostPlayerNotify");
			Console.WriteLine(msgHostPlayerNotify.PacketBody.ToString());

			MsgPlayerEnterSceneInfoNotify msgPlayerEnterSceneInfoNotify = new MsgPlayerEnterSceneInfoNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = PlayerEnterSceneInfoNotify.Parser.ParseFrom(File.ReadAllBytes("PlayerEnterSceneInfoNotify")),
			};

			msgPlayerEnterSceneInfoNotify.PacketBody.EnterSceneToken = 12383;

			await player.Session.SendAsync(msgPlayerEnterSceneInfoNotify.AsBytes());
			Console.WriteLine("msgPlayerEnterSceneInfoNotify");
			Console.WriteLine(msgPlayerEnterSceneInfoNotify.PacketBody.ToString());

			MsgSyncScenePlayTeamEntityNotify msgSyncScenePlayTeamEntityNotify = new MsgSyncScenePlayTeamEntityNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = new SyncScenePlayTeamEntityNotify
                {
					SceneId = 3,
                },
			};

			await player.Session.SendAsync(msgSyncScenePlayTeamEntityNotify.AsBytes());
			Console.WriteLine("msgSyncScenePlayTeamEntityNotify");
			Console.WriteLine(msgSyncScenePlayTeamEntityNotify.PacketBody.ToString());

			MsgScenePlayerInfoNotify msgScenePlayerInfoNotify = new MsgScenePlayerInfoNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = ScenePlayerInfoNotify.Parser.ParseFrom(File.ReadAllBytes("ScenePlayerInfoNotify.bin")),
			};

			await player.Session.SendAsync(msgScenePlayerInfoNotify.AsBytes());
			Console.WriteLine("msgScenePlayerInfoNotify");
			Console.WriteLine(msgScenePlayerInfoNotify.PacketBody.ToString());

			MsgSceneTeamUpdateNotify msgSceneTeamUpdateNotify = new MsgSceneTeamUpdateNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = SceneTeamUpdateNotify.Parser.ParseFrom(File.ReadAllBytes("SceneTeamUpdateNotify.bin")),
			};

			await player.Session.SendAsync(msgSceneTeamUpdateNotify.AsBytes());
			Console.WriteLine("msgSceneTeamUpdateNotify");
			Console.WriteLine(msgSceneTeamUpdateNotify.PacketBody.ToString());

			msgSyncScenePlayTeamEntityNotify = new MsgSyncScenePlayTeamEntityNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = new SyncScenePlayTeamEntityNotify
				{
					SceneId = 3,
				},
			};

			await player.Session.SendAsync(msgSyncScenePlayTeamEntityNotify.AsBytes());
			Console.WriteLine("msgSyncScenePlayTeamEntityNotify");
			Console.WriteLine(msgSyncScenePlayTeamEntityNotify.PacketBody.ToString());

			MsgPlayerGameTimeNotify msgPlayerGameTimeNotify = new MsgPlayerGameTimeNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = new PlayerGameTimeNotify
				{
					Uid = 708845657,
					GameTime = 71727,
				},
			};

			await player.Session.SendAsync(msgPlayerGameTimeNotify.AsBytes());
			Console.WriteLine("msgPlayerGameTimeNotify");
			Console.WriteLine(msgPlayerGameTimeNotify.PacketBody.ToString());

			MsgSceneTimeNotify msgSceneTimeNotify = new MsgSceneTimeNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = new SceneTimeNotify
				{
					SceneId = 3,
				},
			};

			await player.Session.SendAsync(msgSceneTimeNotify.AsBytes());
			Console.WriteLine("msgSceneTimeNotify");
			Console.WriteLine(msgSceneTimeNotify.PacketBody.ToString());

			MsgSceneDataNotify msgSceneDataNotify = new MsgSceneDataNotify
			{
				metaData = new PacketHead
				{
					SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
					ClientSequenceId = req.metaData.ClientSequenceId,
				},
				packetBody = new SceneDataNotify
				{
				},
			};
			await player.Session.SendAsync(msgSceneDataNotify.AsBytes());
			Console.WriteLine("msgSceneDataNotify");
			Console.WriteLine(msgSceneDataNotify.PacketBody.ToString());


			await player.Session.SendAsync(rsp.AsBytes());
			Console.WriteLine("MsgSceneInitFinishRsp");
			Console.WriteLine(rsp.PacketBody.ToString());
			return;
		}
	}
}

