using EmployeeTimeApi.Application.Employees.Dtos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeTimeApi.Application.Employees.Validators;

internal static class Extensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AddEmployeeDto>, AddEmployeeValidator>();
        services.AddTransient<IValidator<UpdateEmployeeDto>, UpdateEmployeeValidator>();

        services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableDataAnnotationsValidation = true;
        });
        return services;
    }
}
