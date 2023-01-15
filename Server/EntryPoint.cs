using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using YSFreedom.Common.Net;
using YSFreedom.Common.Protocol;
using YSFreedom.Common.Scripting;
using YSFreedom.Common.Util;
using YSFreedom.Common.Services;
using YSFreedom.Server.Database;
using YSFreedom.Server.Auth;
using YSFreedom.Server.Game;
using Serilog;
using Jint.Runtime.Interop;

namespace YSFreedom.Server
{
    class Program
    {
        private static long launchTime;
        private static AsyncEngine jsEngine;
        private static Config config;

        // Service modules
        public static ServiceManager serviceManager;
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main";
            launchTime = MonotonicTime.Now;

            serviceManager = new ServiceManager();
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithThreadName()
                .Enrich.WithThreadId()
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId} {ThreadName}) {Message}{NewLine}{Exception}")
                .MinimumLevel.Verbose()
                .CreateLogger();

            Log.Information("Initializing YSFreedom server...");

            string configFileText = @"
                var dbService = service('DatabaseService', {});
                var authService = service('AuthService', {DatabaseService: dbService});
                service('LoginService', {HttpAddr: Address('127.0.0.1:80'), HttpsAddr: Address('127.0.0.1:443'), AuthService: authService});
                service('GameService', {KcpAddr: Address('127.127.127.127:22101'), AuthService: authService});
            ";
            
            if (args.Length > 0)
            {
                try
                {
                    configFileText = File.ReadAllText(args[0]);
                }
                catch (Exception)
                {
                    Log.Fatal("Failed to read configuration file {cfgFile}", args[0]);
                    Environment.Exit(1);
                }
            }

            Log.Information("Registering services...");
            serviceManager.RegisterService("LoginService", LoginService.Create);
            serviceManager.RegisterService("GameService", GameService.Create);
            serviceManager.RegisterService("DatabaseService", DatabaseService.Create);
            serviceManager.RegisterService("AuthService", AuthService.Create);

            Log.Information("Services registered.");

            config = new Config();
            Log.Information("Created default configuration.");

            // TODO: move this (JS related stuff) to another class
            jsEngine = new AsyncEngine(cfg => cfg.AllowClr());
            // Misc functions and types
            jsEngine.SetValue("Address", new Func<string, IPEndPoint>(addr => IPEndPoint.Parse(addr)));
            jsEngine.SetValue("debug", new Action<string>(str => Log.Debug(str)));
            // Add required configuration functions
            jsEngine.SetValue("config", config);
            jsEngine.SetValue("service", new Func<string,IDictionary<string,object>,IService>((nam, cfg) => serviceManager.StartService(nam, cfg)));


            Log.Information("Initialized scripting.");

            try
            {
                jsEngine.Execute(configFileText);
            } catch (Exception exc) {
                Console.WriteLine(exc);
                Log.Fatal("Failed to parse configuration.");
                Environment.Exit(1);
            }

            Log.Information(config.InitNotify);

            Log.Information("Initialized services.");

            serviceManager.WaitAll();
            Log.Information("All services exited.");
            Log.Information("Goodbye.");
        }
    }
}