using EmployeeSync.Data;
using EmployeeSync.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSync.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly EmployeeDbContext dbContext;
        private readonly SemaphoreSlim semaphoreSlim = new(1, 1); // (1, 1) mean initial count and max. count
        public GenericRepository(EmployeeDbContext _dbContext) // Apply Dependency Injection Here
        {
            this.dbContext = _dbContext;
        }
        public async Task AddAsync(T entity)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                await dbContext.Set<T>().AddAsync(entity);
                await dbContext.SaveChangesAsync();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public async Task DeleteAsync(int id)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                var Del = await dbContext.Set<T>().FindAsync(id);  // Null check if data not found in Table
                if (Del != null)
                {
                    dbContext.Set<T>().Remove(Del);
                    await dbContext.SaveChangesAsync();
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                return await dbContext.Set<T>().ToListAsync();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                var entity = await dbContext.Set<T>().FindAsync(id);
                if (entity == null)
                {
                    throw new Exception($"Entity with id {id} not found.");   // handle not found, throw exception or return NotFound           
                }
                return entity;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public async Task UpdateAsync(T entity)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                dbContext.Set<T>().Update(entity);
                await dbContext.SaveChangesAsync();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}