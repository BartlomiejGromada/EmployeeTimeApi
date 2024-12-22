using EmployeeTimeApi.Domain.Accounts.Models;

namespace EmployeeTimeApi.Application.Accounts.Repositories;

internal interface IAccountRepository
{
    Task<bool> IsExistAsync(string email, CancellationToken? cancellationToken);
    Task AddAsync(
        Account account,
        CancellationToken? cancellationToken = default);
    Task<Account?> GetByEmailAsync(
        string email,
        CancellationToken? cancellationToken = default);
}
