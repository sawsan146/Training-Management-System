﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TrainingSys.Core.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);

        Task AddAsync(T entity);

        Task DeleteByIdAsync(int id);

        Task UpdateAsync(T entity);
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);


    }
}
