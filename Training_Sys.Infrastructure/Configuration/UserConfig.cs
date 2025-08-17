using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingSys.Core.Entities;

namespace Training_Sys.Infrastructure.Configuration
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Role).IsRequired();
        }
    }
}
