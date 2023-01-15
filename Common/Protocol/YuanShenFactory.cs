using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YSFreedom.Common.Protocol
{
    public class YuanShenMessageFactory
    {
        static Dictionary<EMsgType, Type> protocolMessages = new Dictionary<EMsgType, Type>();

        public static void InitializeFactory()
        {
            var protocolMessagesTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                type.IsSubclassOf(typeof(YuanShenPacket)) &&
                type.Namespace == "YSFreedom.Common.Protocol.Messages");

            foreach (var h in protocolMessagesTypes)
                if (Enum.TryParse(h.Name.Substring(3, h.Name.Length - 3), out EMsgType typeEnum))
                    protocolMessages.Add(typeEnum, h);
        }

        public static YuanShenPacket NewInstance(EMsgType msgType, byte[] buffer)
        {
            if (protocolMessages.TryGetValue(msgType, out var responseType))
            {
                var constructor = responseType.GetConstructor(new Type[] { typeof(byte[]) });
                return (YuanShenPacket)constructor.Invoke(new object[] { buffer });
            }
            else
                return new YuanShenPacket(buffer);
        }
    }
}
