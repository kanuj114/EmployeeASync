using EmployeeSync.DTO;
using EmployeeSync.Interfaces;
using EmployeeSync.Models;

namespace EmployeeSync.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> repository;
        public EmployeeService(IRepository<Employee> _repository) // Dependency Injection Here
        {
            repository = _repository;
        }
        public async Task AddEmployeeAsync(EmployeeDTO dto)
        {
            var employee = new Employee // Explicit Binding here when not using Automapper
            {
                Name = dto.Name,
                Department = dto.Department,
                Salary = dto.Salary
            };
            await repository.AddAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await repository.DeleteAsync(id);
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDTO dto)
        {
            var exists = await repository.GetByIdAsync(id);
            if (exists != null)
            {
                exists.Name = dto.Name;
                exists.Department = dto.Department;
                exists.Salary = dto.Salary;
                await repository.UpdateAsync(exists);
            }

        }
    }
}