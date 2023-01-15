using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSFreedom.Common.Protocol;
using YSFreedom.Server.Game;

namespace YSFreedom.Server.Protocol
{
    public interface IYuanShenHandler
    {
        Task HandleAsync(YuanShenPacket packet, Player player);
    }
}
