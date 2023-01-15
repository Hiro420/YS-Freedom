using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YSGenerator
{
    class Program
    {
        static String definitionsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "Common", "Protocol", "Definitions");
        static String handlersPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Handlers");
        static List<String> excludedDefinitions = new List<string> { "QueryCurrRegionHttpRsp", "QueryRegionListHttpRsp" };
        static void Main(string[] args)
        {
            if (!Directory.Exists(handlersPath)) Directory.CreateDirectory(handlersPath);

            List<String> definitions = new List<String>();

            foreach (var item in Directory.EnumerateFiles(definitionsPath))
            {
                FileInfo fileInfo = new FileInfo(item);
                definitions.Add(fileInfo.Name.Remove(fileInfo.Name.Length - 6, 6));
            }

            foreach (var definition in definitions)
            {
                Console.WriteLine(definition);

                if (definition.EndsWith("Req") && !excludedDefinitions.Contains(definition))
                {
                    if (definitions.Contains(definition.Replace("Req", "Rsp"))) GenerateHandlerDefinition(definition, definition.Replace("Req", "Rsp"));
                    else GenerateHandlerDefinition(definition);
                }
                     
            }
        }
        static void GenerateHandlerDefinition(String messageName, String responseName = "")
        {
            String handlerBuilderStart = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSFreedom.Common.Protocol;
using YSFreedom.Common.Protocol.Messages;

namespace YSFreedom.Server.Protocol.Handlers
{
	public class @messageNameHandler : IYuanShenHandler
	{
		public @messageNameHandler() { }
		public Task HandleAsync(YuanShenPacket packet)
		{
			Msg@messageName req = (Msg@messageName)packet;";

            String handlerResponseSection = @"
			Msg@responseName rsp = new Msg@responseName
            {
                metaData = new PacketHead
                {
                    SentMs = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    ClientSequenceId = req.metaData.ClientSequenceId,
                }
            };
";
            
            String handlerBuilderEnd = @"
			throw new NotImplementedException();
		}
	}
}
";
            StringBuilder handlerBuilder = new StringBuilder();

            handlerBuilder.AppendLine(handlerBuilderStart.Replace("@messageName", messageName));
            if(!String.IsNullOrWhiteSpace(responseName))
                handlerBuilder.AppendLine(handlerResponseSection.Replace("@responseName", responseName));

            handlerBuilder.AppendLine(handlerBuilderEnd);

            File.WriteAllText(Path.Combine(handlersPath, $"{messageName}Handler.cs"), handlerBuilder.ToString());
        }
    }
}