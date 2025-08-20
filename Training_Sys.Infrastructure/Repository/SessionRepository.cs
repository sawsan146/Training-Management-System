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
    public class SessionRepository:GenericRepository<Session>,ISessionRepository
    {
        private readonly ApplicationDbContext _context;

        public SessionRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
