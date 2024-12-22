using FluentValidation;

namespace EmployeeTimeApi.Application.Accounts.Validators;

internal sealed class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(x => x)
           .NotEmpty()
           .MinimumLength(8)
           .Matches("[A-Z]")
           .Matches("[a-z]")
           .Matches("[0-9]")
           .Matches("[^a-zA-Z0-9]");
    }
}
