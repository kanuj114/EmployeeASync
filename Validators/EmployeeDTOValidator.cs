using EmployeeSync.DTO;
using FluentValidation;

namespace EmployeeSync.Validators
{
    public class EmployeeDTOValidator : AbstractValidator<EmployeeDTO>
    {
        public EmployeeDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Department).NotEmpty().WithMessage("Department is required");
            RuleFor(x => x.Salary).GreaterThan(0).WithMessage("Salary must be positive");
        }
    }
}