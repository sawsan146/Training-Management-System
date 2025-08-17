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
    public class GradeConfig : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.Property(g => g.Value).IsRequired();
            builder.HasOne(g => g.Trainee)
           .WithMany()
           .HasForeignKey(g => g.TraineeId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Session)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
