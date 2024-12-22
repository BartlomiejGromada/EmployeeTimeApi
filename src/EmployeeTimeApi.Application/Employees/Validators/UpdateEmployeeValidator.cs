using EmployeeTimeApi.Application.Employees.Dtos;
using FluentValidation;

namespace EmployeeTimeApi.Application.Employees.Validators;

internal sealed class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeDto>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
    }
}
