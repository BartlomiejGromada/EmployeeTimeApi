using EmployeeTimeApi.Application.Accounts.Services;
using EmployeeTimeApi.Application.Accounts.Validators;
using EmployeeTimeApi.Application.Employees.Services;
using EmployeeTimeApi.Application.Employees.Validators;
using EmployeeTimeApi.Application.TimeEntries.Services;
using EmployeeTimeApi.Application.TimeEntries.Validators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EmployeeTimeApi.Presentation")]
[assembly: InternalsVisibleTo("EmployeeTimeApi.Infrastructure")]
[assembly: InternalsVisibleTo("EmployeeTimeApi.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace EmployeeTimeApi.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IEmployeesService, EmployeesService>();
        services.AddScoped<ITimeEntriesService, TimeEntriesService>();

        services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableDataAnnotationsValidation = true;
        });
        services.AddEmployeesValidators();
        services.AddTimeEntriesValidators();
        services.AddAccountValidators();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
