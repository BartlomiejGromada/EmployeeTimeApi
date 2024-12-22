using EmployeeTimeApi.Shared.Abstractions.Exceptions;

namespace EmployeeTimeApi.Domain.Accounts.Exceptions;

internal class AccountAlreadyExistException : BusinessRuleException
{
    public AccountAlreadyExistException(string email) : base($"Account with email: {email} is already exist.")
    {
    }
}
