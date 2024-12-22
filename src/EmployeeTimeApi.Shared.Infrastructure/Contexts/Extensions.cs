using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace EmployeeTimeApi.Shared.Infrastructure.Contexts;

public static class Extensions
{
    public static IServiceCollection AddContext(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        return services;
    }

    public static int GetAccountId(this ClaimsPrincipal? principal)
    {
        string? accountId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

        return int.TryParse(accountId, out int parsedAccountId) ?
            parsedAccountId :
            throw new ApplicationException("Account id is unavailable");
    }

    public static string GetAccountEmail(this ClaimsPrincipal? principal)
    {
        string? accountEmail = principal?.FindFirstValue(ClaimTypes.Email);

        return accountEmail ??
            throw new ApplicationException("Account email is unavailable");
    }
}
