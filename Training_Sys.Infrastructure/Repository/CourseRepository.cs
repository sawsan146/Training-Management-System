using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training_Sys.Infrastructure.Data;
using TrainingSys.Core.Entities;
using TrainingSys.Core.Interface;

namespace Training_Sys.Infrastructure.Repository
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList< Course>> SearchByNameAsync(string courseName)
        {
            var Courses= await _context.Courses
                    .Where(c => c.Name.ToLower().Contains(courseName.ToLower())).ToListAsync();
            return Courses;
        }
        public async Task<IReadOnlyList<Course>> SearchByCategoryAsync(string categoryName)
        {
            var courses = await _context.Courses
                                .Where(c => c.Category.ToLower().Contains(categoryName.ToLower()))
                                .ToListAsync();
            return courses;
        }
    }
}
