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
    public class UserRepository:GenericRepository<User>,IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<User>> GetAllInstructorsAsync()
        {
            var instructors= await _context.users.Where(u => u.Role == "instructor").ToListAsync();
            return instructors;

        }
    }
}
