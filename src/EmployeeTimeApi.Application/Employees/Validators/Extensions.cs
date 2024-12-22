using EmployeeTimeApi.Application.Employees.Dtos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeTimeApi.Application.Employees.Validators;

internal static class Extensions
{
    public static IServiceCollection AddEmployeesValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AddEmployeeDto>, AddEmployeeValidator>();
        services.AddTransient<IValidator<UpdateEmployeeDto>, UpdateEmployeeValidator>();

        return services;
    }
}
