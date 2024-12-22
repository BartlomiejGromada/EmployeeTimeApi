using EmployeeTimeApi.Shared.Abstractions.Exceptions;

namespace EmployeeTimeApi.Domain.Employees.Exceptions;

internal class EmailAlreadyTakenException : BusinessRuleException
{
    public EmailAlreadyTakenException(string email) : base($"Email: {email} is already taken.")
    {
    }
}
