using Microsoft.EntityFrameworkCore;
using YSFreedom.Server.Database.Configurations;
using YSFreedom.Server.Database.Models;
using YSFreedom.Server.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSFreedom.Server.Database
{
    internal class YuanShenDbContext : DbContext
    {
        public string ConnectionString { get; set; }
        public DbSet<YuanShenUser> Users { get; set; }
        public DbSet<MHYAccount> Accounts { get; set; }

        public YuanShenDbContext() { }
        public YuanShenDbContext(string connectionString) { ConnectionString = connectionString; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply Configurations
            builder.ApplyConfiguration(new YuanShenUserConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(ConnectionString);
        }
    }
}
