using System;

namespace EmployeeSync.Interfaces
{
   public interface IRepository<T> where T : class // Best approach for achieving Loosely Coupled Architecture and follows DRY Principle.
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}