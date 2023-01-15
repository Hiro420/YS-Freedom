using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YSFreedom.Server.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSFreedom.Server.Database.Configurations
{
    internal class YuanShenUserConfiguration : IEntityTypeConfiguration<YuanShenUser>
    {
        public void Configure(EntityTypeBuilder<YuanShenUser> builder)
        {
            builder
                .HasKey(m => m.uid);

            builder
                .Property(m => m.uid);

            builder
                .Property(m => m.name)
                .IsRequired();

            builder
                .Property(m => m.email)
                .IsRequired();

            builder
                .Property(m => m.password)
                .IsRequired();
        }
    }
}
