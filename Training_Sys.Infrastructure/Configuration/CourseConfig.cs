using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TrainingSys.Core.Entities;

namespace Training_Sys.Infrastructure.Configuration
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Category).IsRequired();

            builder.HasOne(c => c.Instructor)
         .WithMany(u => u.Courses) 
         .HasForeignKey(c => c.InstructorID)
         .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
