
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSFreedom.Common.Protocol;
using YSFreedom.Common.Protocol.Messages;
using YSFreedom.Server.Game;
using Google.Protobuf;
using System.IO;

namespace YSFreedom.Server.Protocol.Handlers
{
    public class PlayerLoginReqHandler : IYuanShenHandler
    {
        public PlayerLoginReqHandler() { }
        public async Task HandleAsync(YuanShenPacket packet, Player player)
        {
            MsgPlayerLoginReq req = (MsgPlayerLoginReq)packet;

            MsgActivityScheduleInfoNotify msgActivityScheduleInfoNotify = new MsgActivityScheduleInfoNotify
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
                packetBody = ActivityScheduleInfoNotify.Parser.ParseFrom(File.ReadAllBytes("ActivityScheduleInfoNotify.bin")),
            };

            await player.Session.SendAsync(msgActivityScheduleInfoNotify.AsBytes());

            Console.WriteLine("msgActivityScheduleInfoNotify");
            Console.WriteLine(msgActivityScheduleInfoNotify.PacketBody.ToString());

            MsgPlayerDataNotify msgPlayerDataNotify = new MsgPlayerDataNotify
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
                packetBody = PlayerDataNotify.Parser.ParseFrom(File.ReadAllBytes("PlayerDataNotify.bin")),
            };

            //msgPlayerDataNotify.PacketBody.ServerTime = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            await player.Session.SendAsync(msgPlayerDataNotify.AsBytes());

            Console.WriteLine("msgPlayerDataNotify");
            Console.WriteLine(msgPlayerDataNotify.PacketBody.ToString());

            MsgOpenStateUpdateNotify msgOpenStateUpdateNotify = new MsgOpenStateUpdateNotify
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
                packetBody = OpenStateUpdateNotify.Parser.ParseFrom(File.ReadAllBytes("OpenStateUpdateNotify.bin")),
            };

            await player.Session.SendAsync(msgOpenStateUpdateNotify.AsBytes());

            Console.WriteLine("msgOpenStateUpdateNotify");
            Console.WriteLine(msgOpenStateUpdateNotify.PacketBody.ToString());

            MsgStoreWeightLimitNotify msgStoreWeightLimitNotify = new MsgStoreWeightLimitNotify
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
                packetBody = new StoreWeightLimitNotify
                {
                    StoreType = (StoreType)1,
                    WeightLimit = 30000,
                    MaterialCountLimit = 2000,
                    WeaponCountLimit = 2000,
                    ReliquaryCountLimit = 1000,
                },
            };

            await player.Session.SendAsync(msgStoreWeightLimitNotify.AsBytes());

            Console.WriteLine("msgStoreWeightLimitNotify");
            Console.WriteLine(msgStoreWeightLimitNotify.PacketBody.ToString());

            MsgPlayerStoreNotify msgPlayerStoreNotify = new MsgPlayerStoreNotify
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
                packetBody = PlayerStoreNotify.Parser.ParseFrom(File.ReadAllBytes("PlayerStoreNotify.bin")),
            };

            await player.Session.SendAsync(msgPlayerStoreNotify.AsBytes());

            Console.WriteLine("msgPlayerStoreNotify");
            Console.WriteLine(msgPlayerStoreNotify.PacketBody.ToString());

            var mavatarlist = AvatarDataNotify.Parser.ParseFrom(File.ReadAllBytes("AvatarDataNotify.bin"));
            
            foreach (var en in mavatarlist.AvatarList)
            {
                    for (uint i = 0;  i < 2000; i++)
                    {
                    if ((i >= 70 && i <= 76) || (i >= 1000 && i <= 1006))
                    {
                           Console.WriteLine($"replacing fightpropmap at key: {i}");
                           var mdict = new Dictionary<uint, float>();
                     en.FightPropMap.Remove(i);
                     mdict.Add(i, 80F);
                     en.FightPropMap.Add(mdict);
                    }
                }

                Console.WriteLine(en.FightPropMap.ToString());
            }

            MsgAvatarDataNotify msgAvatarDataNotify = new MsgAvatarDataNotify
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
                packetBody = mavatarlist,
            };

            await player.Session.SendAsync(msgAvatarDataNotify.AsBytes());
            Console.WriteLine("msgAvatarDataNotify");
            Console.WriteLine(msgAvatarDataNotify.PacketBody.ToString());

            MsgAvatarSatiationDataNotify msgAvatarSatiationDataNotify = new MsgAvatarSatiationDataNotify
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
                packetBody = AvatarSatiationDataNotify.Parser.ParseFrom(File.ReadAllBytes("AvatarSatiationDataNotify.bin")),
            };

            await player.Session.SendAsync(msgAvatarSatiationDataNotify.AsBytes());
            Console.WriteLine("msgAvatarSatiationDataNotify");
            Console.WriteLine(msgAvatarSatiationDataNotify.PacketBody.ToString());

            MsgRegionSearchNotify msgRegionSearchNotify = new MsgRegionSearchNotify
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
                packetBody = new RegionSearchNotify
                {
                    Uid = 708845657,
                }
            };

            await player.Session.SendAsync(msgRegionSearchNotify.AsBytes());

            Console.WriteLine("msgRegionSearchNotify");
            Console.WriteLine(msgRegionSearchNotify.PacketBody.ToString());


            MsgPlayerEnterSceneNotify msgPlayerEnterSceneNotify = new MsgPlayerEnterSceneNotify
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
                packetBody = PlayerEnterSceneNotify.Parser.ParseFrom(File.ReadAllBytes("PlayerEnterSceneNotify.bin")),
            };

            msgPlayerEnterSceneNotify.PacketBody.EnterSceneToken = 12383;

            await player.Session.SendAsync(msgPlayerEnterSceneNotify.AsBytes());

            Console.WriteLine("msgPlayerEnterSceneNotify");
            Console.WriteLine(msgPlayerEnterSceneNotify.PacketBody.ToString());

            MsgPlayerLoginRsp rsp = new MsgPlayerLoginRsp
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                },
                packetBody = new PlayerLoginRsp
                {
                    IsUseAbilityHash = true,
                    AbilityHashCode = 1379187098,
                    GameBiz = "hk4e_global",
                    ClientDataVersion = 2316848,
                    ClientSilenceDataVersion = 2316848,
                    ClientMd5 = "{\"fileSize\": 4401, \"remoteName\": \"data_versions\", \"md5\": \"144149abbc0bd5fd5d2a8a42dbe28c7a\"}",
                    ClientSilenceMd5 = "{\"fileSize\": 514, \"remoteName\": \"data_versions\", \"md5\": \"d516b8642fb6af40caa805a337793156\"}",
                    ClientVersionSuffix = "6b1ce6c5b2",
                    ClientSilenceVersionSuffix = "6b1ce6c5b2",
                    CountryCode = player.Account.CountryCode,
                    RegisterCps = "mihoyo",
                    IsScOpen = true,
                    ScInfo = ByteString.FromBase64("zxAB/CgAAAD/VXx9ptBGOXJfySpdYe35hicDFhYfIH3g/r8It1kl4w=="),
                },
            };

            ResVersionConfig resVersionConfig = new ResVersionConfig
            {
                Branch = "1.4_live",
                Md5 = "{\"remoteName\": \"res_versions_external\", \"md5\": \"6bf780879dc428622eb0cf5e4a7bd480\", \"fileSize\": 257189}\r\n{\"remoteName\": \"res_versions_medium\", \"md5\": \"ba2b214d3884085c73590351753f36f5\", \"fileSize\": 126497}\r\n{\"remoteName\": \"res_versions_streaming\", \"md5\": \"3365372cce0ff35b137f216923cce24c\", \"fileSize\": 2280}\r\n{\"remoteName\": \"release_res_versions_external\", \"md5\": \"962ccc4ed9863dd0ddc319277b0df243\", \"fileSize\": 257189}\r\n{\"remoteName\": \"release_res_versions_medium\", \"md5\": \"e2c69c4ea86bc440a5b2a2087224b235\", \"fileSize\": 126497}\r\n{\"remoteName\": \"release_res_versions_streaming\", \"md5\": \"687c9fc4517832cde8474eacdb618933\", \"fileSize\": 2280}\r\n{\"remoteName\": \"base_revision\", \"md5\": \"d41d8cd98f00b204e9800998ecf8427e\", \"fileSize\": 0}",
                ReleaseTotalSize = "0",
                VersionSuffix = "7e09fd6db0"
            };

            rsp.PacketBody.ResVersionConfig = resVersionConfig;

            await player.Session.SendAsync(rsp.AsBytes());

            Console.WriteLine("rsp");
            Console.WriteLine(rsp.PacketBody.ToString());
        }
    }
}

