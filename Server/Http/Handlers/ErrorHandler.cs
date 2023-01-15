using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using uhttpsharp;

namespace YSFreedom.Server.HttpApi.Handlers.Handlers
{
    public class ErrorHandler : IHttpRequestHandler
    {
        public Task Handle(IHttpContext context, System.Func<Task> next)
        {
            context.Response = new HttpResponseError(context.Request, HttpResponseCode.NotFound);
            return Task.Factory.GetCompleted();
        }
    }
}
