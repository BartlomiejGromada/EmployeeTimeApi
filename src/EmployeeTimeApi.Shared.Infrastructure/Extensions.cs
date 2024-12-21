using Microsoft.Extensions.Configuration;

namespace EmployeeTimeApi.Shared.Infrastructure;

public static class Extensions
{
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}
