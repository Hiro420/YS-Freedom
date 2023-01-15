﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using uhttpsharp;

namespace YSFreedom.Server.HttpApi.Handlers.Handlers
{
    public class ExceptionHandler : IHttpRequestHandler
    {
        public async Task Handle(IHttpContext context, Func<Task> next)
        {
            try
            {
                await next().ConfigureAwait(false);
            }
            catch (HttpException e)
            {
                context.Response = new HttpResponse(e.ResponseCode, "Error while handling your request. " + e.Message, false);
            }
            catch (Exception e)
            {
                context.Response = new HttpResponse(HttpResponseCode.InternalServerError, $"Error while handling your request. \n {e}", false);
            }
        }
    }
}
