using EmployeeSync.DTO;
using EmployeeSync.Models;

namespace EmployeeSync.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(EmployeeDTO dto);
        Task UpdateEmployeeAsync(int id, EmployeeDTO dto);
        Task DeleteEmployeeAsync(int id);
    }
}