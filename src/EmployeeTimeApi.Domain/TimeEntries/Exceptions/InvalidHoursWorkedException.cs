using EmployeeTimeApi.Shared.Abstractions.Exceptions;

namespace EmployeeTimeApi.Domain.TimeEntries.Exceptions;

internal class InvalidHoursWorkedException : BusinessRuleException
{
    public InvalidHoursWorkedException() : base($"Hours worked must be between 1 and 24.")
    {
    }
}
