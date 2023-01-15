using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using YSFreedom.Server.Database;
using YSFreedom.Server.Database.Models;
using YSFreedom.Common.Services;
using System.Collections.Generic;
using Serilog;

namespace YSFreedom.Server.Database
{
    public class DatabaseService : Service
    {
        public class Config
        {
            public string ConnectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.db")}";
        }

        public Config config = new Config();

        internal YuanShenDbContext DB { get { return dbContext; } }
        internal YuanShenDbContext dbContext;

        public override Task Start()
        {
            if (task != null) return task;
            return task = Task.Run(Main);
        }

        public async void Main()
        {
            Thread.CurrentThread.Name = "DatabaseService";
            _State = ServiceState.RUNNING;
            Log.Information("Initializing...");
            Initialize();

            await dbContext.Database.EnsureCreatedAsync();

            if ((await dbContext.Accounts.FindAsync((ulong)708845657)) == null)
                await dbContext.AddAsync(new YSFreedom.Server.Auth.MHYAccount() { UID = 708845657, Email = "Leonardomeitz@gmail.com", Password = "test", CountryCode = "DE", NickName = "Fujiwara" });
            await dbContext.SaveChangesAsync();

            Log.Information("Ready");
            while (_State == ServiceState.RUNNING)
                await Task.Delay(1000);

            dbContext.Dispose();
            _State = ServiceState.STOPPED;
        }

        public override void Configure(object newConfig)
        {
            config = ((IDictionary<string,object>)newConfig).ToObject<Config>();
        }

        private void Initialize()
        {
            dbContext = new YuanShenDbContext(config.ConnectionString);
        }

        public static IService Create() => new DatabaseService();
    }
}