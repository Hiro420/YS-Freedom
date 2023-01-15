using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSFreedom.Common.Protocol;
using YSFreedom.Common.Protocol.Messages;
using YSFreedom.Server.Game;
using YSFreedom.Server.Auth;
using YSFreedom.Common.Crypto;
using Google.Protobuf;

namespace YSFreedom.Server.Protocol.Handlers
{
    public class GetPlayerTokenReqHandler : IYuanShenHandler
    {
        public GetPlayerTokenReqHandler() { }
        public async Task HandleAsync(YuanShenPacket packet, Player player)
        {
            MsgGetPlayerTokenReq req = (MsgGetPlayerTokenReq)packet;

            MsgGetPlayerTokenRsp rsp = new MsgGetPlayerTokenRsp
            {
                metaData = new PacketHead
                {

                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                }
            };

            player.Account = await player.AuthCalls.GetAccountByUID(Convert.ToUInt64(req.PacketBody.AccountUid));
            ulong secretKeySeed = 0xCAFE77ABAC05BEEF;

            var rspBody = new GetPlayerTokenRsp()
            {
                Uid = (uint)player.Account.UID,
                Token = req.PacketBody.AccountToken,
                AccountType = req.PacketBody.AccountType,
                CountryCode = req.PacketBody.CountryCode,
                PlatformType = req.PacketBody.PlatformType,
                SecretKeySeed = secretKeySeed,
                SecurityCmdBuffer = ByteString.CopyFrom(new byte[32]),
            };
            rsp.packetBody = rspBody;

            await player.Session.SendAsync(rsp.AsBytes());
            Console.WriteLine("after send async");
            player.Session.Key = new Common.Crypto.YuanShenKey(secretKeySeed);
        }
    }
}