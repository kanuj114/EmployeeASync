using EmployeeSync.DTO;
using EmployeeSync.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await employeeService.GetEmployeesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await employeeService.GetEmployeeByIdAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDTO dTO)
        {
            await employeeService.AddEmployeeAsync(dTO);
            return Ok();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDTO dTO)
        {
            await employeeService.UpdateEmployeeAsync(id, dTO);
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }

    }
}
