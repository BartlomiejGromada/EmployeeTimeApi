using EmployeeTimeApi.Shared.Abstractions.Exceptions;

namespace EmployeeTimeApi.Domain.TimeEntries.Exceptions;

internal class EmployeeTimeEntryMismatchException : BusinessRuleException
{
    public EmployeeTimeEntryMismatchException(int employeeId, int timeEntryId) : base($"Time entry with id: " +
        $"{timeEntryId} doesn't belong to employee with id: {employeeId}")
    {
    }
}
