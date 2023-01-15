
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
	public class CombatInvocationsNotifyHandler : IYuanShenHandler
	{
		public CombatInvocationsNotifyHandler() { }
		public async Task HandleAsync(YuanShenPacket packet, Player player)
		{
			MsgCombatInvocationsNotify req = (MsgCombatInvocationsNotify)packet;

			Console.WriteLine(req.ToString());

			foreach (var item in req.PacketBody.InvokeList)
            {
				Console.WriteLine($"got CombatInvocationsNotify with argument_type: {(EMsgType)item.ArgumentType}");
            }

			return;
		}
	}
}

