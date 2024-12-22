
namespace EmployeeTimeApi.Domain.Accounts.Exceptions;

internal class UnauthorizedAccessAccountException : UnauthorizedAccessException
{
    public UnauthorizedAccessAccountException(string requestingEmail, string targetEmailAddress)
          : base($"Unauthorized access: the requesting email '{requestingEmail}' does not have permission to access the account with email '{targetEmailAddress}'.")
    {
    }
}
