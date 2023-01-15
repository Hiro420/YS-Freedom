using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using uhttpsharp;
using uhttpsharp.Headers;
using YSFreedom.Common.Util;

namespace YSFreedom.Server.HttpApi.vHost
{
    internal class BaseController : IHttpRequestHandler
    {
        private HttpResponse _response;
        private HttpResponse _keepAliveResponse;

        protected readonly IDictionary<string, Func<IHttpContext, Func<Task>, Task>> _handlers = new Dictionary<string, Func<IHttpContext, Func<Task>, Task>>(StringComparer.InvariantCultureIgnoreCase);

        public async virtual Task Handle(IHttpContext context, Func<Task> nextHandler)
        {
            Func<IHttpContext, Func<Task>, Task> value;

            if (_handlers.TryGetValue(context.Request.Uri.OriginalString, out value))
            {
                await value.Invoke(context, nextHandler);
                return;
            }

            // Route not found, Call next.
            await nextHandler();
        }

        protected IHttpResponse GetBaseResponse(IHttpRequest request, String response)
        {
            _keepAliveResponse = new HttpResponse(HttpResponseCode.Ok, response, true);
            _response = new HttpResponse(HttpResponseCode.Ok, response, false);

            return request.Headers.KeepAliveConnection() ? _keepAliveResponse : _response;
        }
        protected IHttpResponse GetBase64Response(IHttpRequest request, byte[] buffer)
        {
            String base64 = Convert.ToBase64String(buffer);

            _keepAliveResponse = new HttpResponse(HttpResponseCode.Ok, base64, true);
            _response = new HttpResponse(HttpResponseCode.Ok, base64, false);

            return request.Headers.KeepAliveConnection() ? _keepAliveResponse : _response;
        }
        protected IHttpResponse GetJsonResponse(IHttpRequest request, object model)
        {
            Stream stream = RecyclableMemoryStream.Create(Encoding.UTF8.GetBytes((JsonConvert.SerializeObject(model))));

            _keepAliveResponse = new HttpResponse(HttpResponseCode.Ok, "application/json; charset=utf-8", stream, true);
            _response = new HttpResponse(HttpResponseCode.Ok, "application/json; charset=utf-8", stream, false);

            return request.Headers.KeepAliveConnection() ? _keepAliveResponse : _response;
        }
    }
}
