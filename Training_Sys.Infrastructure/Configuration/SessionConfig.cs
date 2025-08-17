using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingSys.Core.Entities;

namespace Training_Sys.Infrastructure.Configuration
{
    public class SessionConfig : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.Property(s => s.StartDate).IsRequired();
            builder.Property(s => s.EndDate).IsRequired();

        }

    }
}
