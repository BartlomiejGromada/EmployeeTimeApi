using EmployeeTimeApi.Shared.Abstractions.Exceptions;

namespace EmployeeTimeApi.Domain.TimeEntries.Exceptions;

internal class TimeEntryAlreadyExistsOnDateException : BusinessRuleException
{
    public TimeEntryAlreadyExistsOnDateException(int employeeId, DateTime date) : base($"Employee with id: {employeeId}" +
        $" already has time entry on date: {date:dd-MM-yyyy}")
    {
    }
}
