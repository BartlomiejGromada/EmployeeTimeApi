using EmployeeTimeApi.Application.Accounts.Repositories;
using EmployeeTimeApi.Application.Employees.Repositories;
using EmployeeTimeApi.Application.TimeEntries.Repositories;
using EmployeeTimeApi.Domain.Accounts.Models;
using EmployeeTimeApi.Infrastructure.Postgres;
using EmployeeTimeApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EmployeeTimeApi.Presentation")]
[assembly: InternalsVisibleTo("EmployeeTimeApi.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace EmployeeTimeApi.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<INpgsqlConnectionFactory, NpgsqlConnectionFactory>();

        services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        services.AddScoped<ITimeEntriesRepository, TimeEntriesRepository>();

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }
}
