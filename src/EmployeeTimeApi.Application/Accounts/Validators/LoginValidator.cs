using EmployeeTimeApi.Application.Accounts.Dtos;
using EmployeeTimeApi.Application.Accounts.Validators;
using FluentValidation;

namespace EmployeeTimeApi.Application.Accountss.Validators;

internal sealed class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).SetValidator(new PasswordValidator());
    }
}
