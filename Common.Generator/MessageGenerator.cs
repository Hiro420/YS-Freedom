using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Text;
using YSFreedom.Common.Protocol;

namespace Common.Generator
{
    [Generator]
    public class MessageGenerator : ISourceGenerator
    {
        static List<String> excludedDefinitions = new List<String> { "Invalid", "QueryCurrRegionHttpRsp", "QueryRegionListHttpRsp" };
        public void Initialize(GeneratorInitializationContext context) { }
        public static Dictionary<string, string> GenerateBuilder()
        {
            var classToBuilder = new Dictionary<string, string>();

            var messageBuilder = @"
using System;
using Google.Protobuf;
using Newtonsoft.Json;

namespace YSFreedom.Common.Protocol.Messages
{
	public class Msg@messageName : YuanShenPacket
	{
		public @messageName PacketBody { get { return packetBody; } }
		public @messageName packetBody;

		public Msg@messageName() : base(EMsgType.@messageName) { }
		public Msg@messageName(byte[] incomingData) : base(incomingData) { }
		public override int GetDataLen() { return packetBody == null ? 0 : packetBody.CalculateSize(); }
		protected override void EncodeData(ref byte[] buffer, Int32 offset) { packetBody.ToByteArray().CopyTo(buffer, offset); }
		protected override void DecodeData(byte[] buffer, Int32 offset, UInt32 dataLen) { packetBody = @messageName.Parser.ParseFrom(buffer, offset, (Int32)dataLen); }
		public override byte[] AsBytes(bool includeData = true)
		{
			if (packetBody != null)
				data = packetBody.ToByteArray();
			var ret = new byte[includeData ? Length : (HEADER_LEN + MAGIC_LEN)];
			Encode(ret, includeData);
			if (includeData)
			{
				if (metaData != null) metaData.ToByteArray().CopyTo(ret, MAGIC_LEN + HEADER_LEN);
				if (packetBody != null) data.CopyTo(ret, MAGIC_LEN + HEADER_LEN + (metaData != null ? metaData.CalculateSize() : 0));
			}
			return ret;
		}
        protected override String DumpBody()
        {
            var _packetBody = JsonConvert.DeserializeObject(packetBody.ToString());
            return JsonConvert.SerializeObject(_packetBody, Formatting.Indented);
        }
	}
}";
            foreach (Int32 msgType in Enum.GetValues(typeof(EMsgType)))
            {
                if(!excludedDefinitions.Contains(((EMsgType)msgType).ToString()))
                    classToBuilder[((EMsgType)msgType).ToString()] = messageBuilder.Replace("@messageName", ((EMsgType)msgType).ToString());
            }

            return classToBuilder;
        }
        public void Execute(GeneratorExecutionContext context)
        {
            foreach (var classBuilder in GenerateBuilder())
            {
                context.AddSource($"Msg{classBuilder.Key}.cs", SourceText.From(classBuilder.Value, Encoding.UTF8));
            }
        }
    }
}
