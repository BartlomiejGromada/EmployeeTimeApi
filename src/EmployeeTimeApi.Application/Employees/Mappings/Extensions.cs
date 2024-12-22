using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EmployeeTimeApi.Application.Employees.Mappings;

internal static class Extensions
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
