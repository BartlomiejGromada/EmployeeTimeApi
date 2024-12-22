using EmployeeTimeApi.Application.Employees.Dtos;
using EmployeeTimeApi.Application.Employees.Mappings;
using EmployeeTimeApi.Application.Employees.Services;
using EmployeeTimeApi.Application.Employees.Validators;
using EmployeeTimeApi.Application.TimeEntries.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
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
        services.AddScoped<IEmployeesService, EmployeesService>();
        services.AddScoped<ITimeEntriesService, TimeEntriesService>();

        services.AddValidators();
        services.AddMappings();

        return services;
    }
}
