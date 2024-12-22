using EmployeeTimeApi.Application.Accounts.Dtos;
using FluentValidation;

namespace EmployeeTimeApi.Application.Accounts.Validators;

internal sealed class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).SetValidator(new PasswordValidator());
        RuleFor(x => x.RepeatPassword).Equal(x => x.Password);
    }
}
