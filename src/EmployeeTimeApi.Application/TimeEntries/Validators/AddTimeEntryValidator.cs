using EmployeeTimeApi.Application.Employees.Dtos;
using EmployeeTimeApi.Application.TimeEntries.Dtos;
using FluentValidation;

namespace EmployeeTimeApi.Application.TimeEntries.Validators;

internal sealed class AddTimeEntryValidator : AbstractValidator<AddTimeEntryDto>
{
    public AddTimeEntryValidator()
    {
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.HoursWorked).InclusiveBetween(1, 24);
    }
}
