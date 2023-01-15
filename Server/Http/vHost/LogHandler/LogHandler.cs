using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uhttpsharp;
using YSFreedom.Server.HttpApi.vHost;

namespace Server.Http.vHost.LogHandler
{
    internal class LogHandler : BaseController
    {
        public LogHandler()
        {
            _handlers.Add("/crash/dataUpload", DumpLog);
        }

        private async Task DumpLog(IHttpContext context, Func<Task> nextHandler)
        {
            Console.WriteLine($"got crash dump: {Encoding.ASCII.GetString(context.Request.Post.Raw)}");
            return;
        }
    }
}
