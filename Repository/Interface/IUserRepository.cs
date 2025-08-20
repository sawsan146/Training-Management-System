using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingSys.Core.Entities;

namespace TrainingSys.Core.Interface
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<IReadOnlyList<User>> GetAllInstructorsAsync();
    }
}
