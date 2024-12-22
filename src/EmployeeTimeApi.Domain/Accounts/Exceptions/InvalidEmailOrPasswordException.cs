using EmployeeTimeApi.Shared.Abstractions.Exceptions;

namespace EmployeeTimeApi.Domain.Accounts.Exceptions;

internal class InvalidEmailOrPasswordException : BusinessRuleException
{
    public InvalidEmailOrPasswordException() : base("Invalid email or password.")
    {
    }
}
