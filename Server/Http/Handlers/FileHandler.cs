using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using uhttpsharp;
using uhttpsharp.Headers;

namespace YSFreedom.Server.HttpApi.Handlers.Handlers
{
    class FileHandler : IHttpRequestHandler
    {
        public static string DefaultMimeType { get; set; }
        public static string HttpRootDirectory { get; set; }
        public static IDictionary<string, string> MimeTypes { get; private set; }

        static FileHandler()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "www")))
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "www"));

            DefaultMimeType = "application/octet-stream";
            MimeTypes = new Dictionary<string, string>
                            {
                                {".css", "text/css"},
                                {".gif", "image/gif"},
                                {".htm", "text/html"},
                                {".html", "text/html"},
                                {".jpg", "image/jpeg"},
                                {".js", "application/javascript"},
                                {".png", "image/png"},
                                {".xml", "application/xml"},
                            };
        }

        private string GetContentType(string path)
        {
            var extension = Path.GetExtension(path) ?? string.Empty;
            if (MimeTypes.ContainsKey(extension))
                return MimeTypes[extension];
            return DefaultMimeType;
        }

        public async Task Handle(IHttpContext context, Func<Task> next)
        {
            string host = "";
            context.Request.Headers.TryGetByName("Host", out host);

            var requestPath = context.Request.Uri.OriginalString.TrimStart('/');
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", host, requestPath);

            if (!File.Exists(path))
            {
                await next().ConfigureAwait(false);
                return;
            }

            context.Response = new HttpResponse(GetContentType(path), File.OpenRead(path), context.Request.Headers.KeepAliveConnection());
            await new TaskFactory().GetCompleted();
        }
    }
}
