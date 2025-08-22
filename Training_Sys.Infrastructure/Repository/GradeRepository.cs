using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Training_Sys.Infrastructure.Data;
using TrainingSys.Core.Entities;
using TrainingSys.Core.Interface;

namespace Training_Sys.Infrastructure.Repository
{
    public class GradeRepository:GenericRepository<Grade>,IGradeRepository
    {
        private readonly ApplicationDbContext _context;

        public GradeRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
      
    }
}
