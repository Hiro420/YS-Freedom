using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Authentication;
using YSFreedom.Common.Services;
using YSFreedom.Server.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace YSFreedom.Server.Auth
{
    public class AuthService : Service, IAuthCalls
    {
        public class Config
        {
            public DatabaseService DatabaseService;
        }

        public Config config = new Config();
        private YuanShenDbContext DB { get { return config.DatabaseService.DB; } }

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
            Thread.CurrentThread.Name = "AuthService";
            _State = ServiceState.RUNNING;
            Log.Information("Initializing...");

            Log.Information("Ready");
            while (_State == ServiceState.RUNNING)
                await Task.Delay(1000);
        }

        public static IService Create() => new AuthService();

        // AuthService public API
        public async Task<MHYAccount> Login(string email, string password)
        {
            try
            {
                var account = await DB.Accounts.FirstAsync(x => x.Email == email);
                if (password != null && !account.VerifyPassword(password))
                    throw new InvalidCredentialException("Invalid password specififed.");

                return account;
            } catch (InvalidOperationException)
            {
                throw new KeyNotFoundException("No user could be located for the specified email address.");
            }
        }

        public async Task<MHYAccount> GetAccountByUID(ulong uid)
        {
            var account = await DB.Accounts.FindAsync(uid);
            if (account == null)
                throw new KeyNotFoundException("No user could be located for the specified UID.");

            return account;
        }
    }
}