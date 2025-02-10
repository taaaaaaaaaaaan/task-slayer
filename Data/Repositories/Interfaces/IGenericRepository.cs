using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_slayer.Data.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}