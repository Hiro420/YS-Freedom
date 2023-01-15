using Amadeus.uHttp.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uhttpsharp.Handlers.Virtualhost
{
    public class VirtualHostRouter : IHttpRequestHandler
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        private readonly IDictionary<string, IHttpRequestHandler> _handlers = new Dictionary<string, IHttpRequestHandler>(StringComparer.InvariantCultureIgnoreCase);

        public VirtualHostRouter(IDictionary<string, IHttpRequestHandler> handlers)
        {
            _handlers = handlers;

            foreach (var vHost in _handlers) Logger.Debug($"Registered virtual host: {vHost.Key}");
        }

        public Task Handle(IHttpContext context, Func<Task> nextHandler)
        {
            string host = "";
            context.Request.Headers.TryGetByName("Host", out host);

            IHttpRequestHandler value;
            if (_handlers.TryGetValue(host, out value)) return value.Handle(context, nextHandler);

            // Route not found, Call next.
            return nextHandler();
        }
    }
}
