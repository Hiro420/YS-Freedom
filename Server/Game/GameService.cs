using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using YSFreedom.Common.Services;
using YSFreedom.Common.Net;
using YSFreedom.Common.Crypto;
using YSFreedom.Common.Protocol;
using YSFreedom.Common.Protocol.Messages;
using YSFreedom.Common.Util;
using YSFreedom.Server.Protocol;
using Google.Protobuf;
using YSFreedom.Server.Auth;
using Serilog;

namespace YSFreedom.Server.Game
{
    public class GameService : Service
    {
        public class Config
        {
            public IPEndPoint KcpAddr = new IPEndPoint(IPAddress.Loopback, 22101);
            public AuthService AuthService;
        }

        public Config config = new Config();
        private KCPServer kcpServer;

        public override Task Start()
        {
            if (task != null) return task;
            return task = Task.Run(Main);
        }

        public override void Configure(object newConfig)
        {
            config = ((IDictionary<string,object>)newConfig).ToObject<Config>();
        }

        public async void Main()
        {
            Thread.CurrentThread.Name = "GameService";
            Log.Information("Initializing...");
            _State = ServiceState.RUNNING;

            YuanShenMessageFactory.InitializeFactory();
            YuanShenHandlerFactory.InitializeFactory();

            kcpServer = new KCPServer(config.KcpAddr);

            Log.Information("Ready.");

            while (_State == ServiceState.RUNNING)
            {
                try
                {
                    var ret = await kcpServer.AcceptAsync();
                    Log.Debug("New connection from {remoteEp}.", ret.RemoteEndpoint);

                    HandleClient(ret.Connection, ret.RemoteEndpoint);
                } catch (Exception ex)
                {
                    Log.Debug("Internal error: {exc}", ex);
                }
            }
        }

        private async void HandleClient(KCP conn, IPEndPoint addr)
        {
            Player player = new Player(conn) {
                AuthCalls = config.AuthService,
            };
            while (conn.State == KCP.ConnectionState.CONNECTED)
            {
                try 
                {
                    await player.HandlePacketAsync(await player.Session.ReceiveAsync());
                } catch (Exception e)
                {
                    Console.WriteLine(e);
                    conn.Close();
                    break;
                }
            }

            conn.Dispose();
        }

        public static IService Create() => new GameService();
    }
}