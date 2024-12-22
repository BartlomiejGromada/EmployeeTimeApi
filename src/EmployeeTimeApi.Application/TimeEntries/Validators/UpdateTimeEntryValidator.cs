using EmployeeTimeApi.Application.TimeEntries.Dtos;
using FluentValidation;

namespace EmployeeTimeApi.Application.TimeEntries.Validators;

internal sealed class UpdateTimeEntryValidator : AbstractValidator<UpdateTimeEntryDto>
{
    public UpdateTimeEntryValidator()
    {
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.HoursWorked)
            .NotEmpty()
            .InclusiveBetween(1, 24);
    }
}
