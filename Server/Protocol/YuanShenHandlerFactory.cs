using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YSFreedom.Common.Protocol;

namespace YSFreedom.Server.Protocol
{
    public class YuanShenHandlerFactory
    {
        static Dictionary<EMsgType, Type> protocolHandlers = new Dictionary<EMsgType, Type>();

        public static void InitializeFactory()
        {
            var protocolHandlerTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                type.Namespace == "YSFreedom.Server.Protocol.Handlers");

            foreach (var h in protocolHandlerTypes)
                if (Enum.TryParse(h.Name.Substring(0, h.Name.Length - 7), out EMsgType typeEnum))
                    protocolHandlers.Add(typeEnum, h);
        }
        public static IYuanShenHandler NewInstance(EMsgType msgType)
        {
            if (protocolHandlers.TryGetValue(msgType, out var responseType))
            {
                var constructor = responseType.GetConstructor(new Type[] { });
                return (IYuanShenHandler)constructor.Invoke(new object[] { });
            }
            else
            {
                Console.WriteLine($"Packet {msgType} not yet implemented");
                return null;
            }
        }
    }
}
