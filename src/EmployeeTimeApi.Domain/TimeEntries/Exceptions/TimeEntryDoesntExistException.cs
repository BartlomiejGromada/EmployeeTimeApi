using EmployeeTimeApi.Shared.Abstractions.Exceptions;

namespace EmployeeTimeApi.Domain.TimeEntries.Exceptions;

internal class TimeEntryDoesntExistException : BusinessRuleException
{
    public TimeEntryDoesntExistException(int entryTimeId) : base($"Time entry with id: {entryTimeId} " +
        $"doesnt exist")
    {
    }
}
