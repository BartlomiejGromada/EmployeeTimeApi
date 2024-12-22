using EmployeeTimeApi.Application.Employees.Dtos;
using FluentValidation;

namespace EmployeeTimeApi.Application.Employees.Validators;

internal sealed class AddEmployeeValidator : AbstractValidator<AddEmployeeDto>
{
    public AddEmployeeValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
    }
}
