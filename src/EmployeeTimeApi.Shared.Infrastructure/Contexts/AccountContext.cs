using EmployeeTimeApi.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Http;

namespace EmployeeTimeApi.Shared.Infrastructure.Contexts;

public sealed class AccountContext : IAccountContext
{
    private IHttpContextAccessor _httpContextAccessor;

    public AccountContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public int AccountId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetAccountId() ??
        throw new ApplicationException("User context is unavailable.");

    public bool IsAuthenticated =>
        _httpContextAccessor
            .HttpContext?
            .User
            .Identity?
            .IsAuthenticated ??
        throw new ApplicationException("User context is unavailable.");
}
