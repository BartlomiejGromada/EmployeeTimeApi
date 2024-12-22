using EmployeeTimeApi.Shared.Abstractions.Exceptions;

namespace EmployeeTimeApi.Domain.Accounts.Exceptions;

internal class AccountDoesntExistException : BusinessRuleException
{
    public AccountDoesntExistException(string email) : base($"Account with email: {email} doesnt exist.")
    {
    }
}
