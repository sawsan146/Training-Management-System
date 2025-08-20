using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingSys.Core.Entities;

namespace TrainingSys.Core.Interface
{
    public interface ICourseRepository:IGenericRepository<Course>
    {
        Task<IReadOnlyList<Course>> SearchByNameAsync(string courseName);
        Task<IReadOnlyList<Course>> SearchByCategoryAsync(string categoryName);
    }
}
