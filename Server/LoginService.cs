using System;
using YSFreedom.Common.Util;
using YSFreedom.Common.Protocol;
using YSFreedom.Server.HttpApi.Handlers.Handlers;
using YSFreedom.Server.HttpApi.vHost.TakumiApi;
using YSFreedom.Server.HttpApi.vHost.YuanshenDispatcher;
using YSFreedom.Server.HttpApi.vHost.hk4eSdk;
using Server.Http.vHost.LogHandler;
using YSFreedom.Common.Services;
using YSFreedom.Server.Auth;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using Serilog;
using uhttpsharp;
using uhttpsharp.Handlers.Compression;
using uhttpsharp.Handlers.Virtualhost;
using uhttpsharp.Listeners;
using uhttpsharp.RequestProviders;

namespace YSFreedom.Server
{
    public class LoginService : Service
    {
        // Public configuration
        public class Config
        {
            public IPEndPoint HttpAddr = new IPEndPoint(IPAddress.Loopback, 80);
            public IPEndPoint HttpsAddr = new IPEndPoint(IPAddress.Loopback, 443);
            public string TlsCert = "YuanShen.p12";
            public AuthService AuthService;
        }

        public Config config = new Config();

        private HttpServer httpServer;
        public override Task Start()
        {
            if (task != null) return task;
            return task = Task.Run(Main);
        }

        public override void Configure(object newConfig)
        {
            config = ((IDictionary<string,object>)newConfig).ToObject<Config>();
        }

        private async Task Main()
        {
            /*
                # sample HOSTS file for GenshinImpact.

                127.0.0.1 log-upload-os.mihoyo.com
                127.0.0.1 overseauspider.yuanshen.com

                # MiHoYo server DNS
                127.0.0.1 api-os-takumi.mihoyo.com
                127.0.0.1 dispatchosglobal.yuanshen.com
                127.0.0.1 osusadispatch.yuanshen.com
                127.0.0.1 hk4e-sdk-os.mihoyo.com
                127.0.0.1 sdk-os-static.mihoyo.com
            */

            Thread.CurrentThread.Name = "LoginService";
            Log.Information("Initializing...");
            _State = ServiceState.RUNNING;

            byte[] p12Data = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.TlsCert));
            X509Certificate serverCertificate = new X509Certificate2(p12Data, "", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

            httpServer = new HttpServer(new HttpRequestProvider());
            httpServer.Use(new TcpListenerAdapter(new TcpListener(config.HttpAddr)));
            httpServer.Use(new ListenerSslDecorator(new TcpListenerAdapter(new TcpListener(config.HttpsAddr)), serverCertificate));

            httpServer.Use(new ExceptionHandler());
            httpServer.Use(new CompressionHandler(DeflateCompressor.Default, GZipCompressor.Default));

            Log.Information("Listening on {httpAddr} (http) and {httpsAddr} (https).", config.HttpAddr.ToString(), config.HttpsAddr.ToString());

            // vHosts
            Dictionary<string, IHttpRequestHandler> vHosts = new Dictionary<string, IHttpRequestHandler>();

            vHosts.Add("api-os-takumi.mihoyo.com", new api_os_takumi());
            vHosts.Add("dispatchosglobal.yuanshen.com", new dispatch_yuanshen());
            vHosts.Add("osusadispatch.yuanshen.com", new dispatch_yuanshen());
            vHosts.Add("hk4e-sdk-os.mihoyo.com", new hk4eSdk(config.AuthService));
            vHosts.Add("sdk-os-static.mihoyo.com", new hk4eSdk(config.AuthService));
            vHosts.Add("log-upload-os.mihoyo.com", new LogHandler());

            VirtualHostRouter vhostRouter = new VirtualHostRouter(vHosts);

            httpServer.Use(vhostRouter);
            httpServer.Use(new FileHandler());
            httpServer.Use(new ErrorHandler());

            httpServer.Start();
            Log.Information("Ready.");

            while (_State == ServiceState.RUNNING)
                await Task.Delay(500);

            httpServer.Dispose();
            _State = ServiceState.STOPPED;
        }

        public static LoginService Create() => new LoginService();
    }
}

