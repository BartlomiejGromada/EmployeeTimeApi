using EmployeeTimeApi.Application.Employees.Services;
using EmployeeTimeApi.Application.TimeEntries.Services;
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

        return services;
    }
}
