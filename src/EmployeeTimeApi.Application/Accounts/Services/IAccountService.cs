using EmployeeTimeApi.Application.Accounts.Dtos;

namespace EmployeeTimeApi.Application.Accounts.Services;

internal interface IAccountService
{
    Task RegisterAsync(
        RegisterDto dto,
        CancellationToken? cancellationToken = default);
    Task<string> GenerateTokenAsync(
        LoginDto dto,
        CancellationToken? cancellationToken = default);
    void Signout(int accountId);
}
