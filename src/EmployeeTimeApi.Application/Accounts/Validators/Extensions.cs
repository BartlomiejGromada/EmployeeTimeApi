using EmployeeTimeApi.Application.Accounts.Dtos;
using EmployeeTimeApi.Application.Accountss.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeTimeApi.Application.Accounts.Validators;

internal static class Extensions
{
    public static IServiceCollection AddAccountValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<LoginDto>, LoginValidator>();
        services.AddTransient<IValidator<RegisterDto>, RegisterValidator>();

        return services;
    }
}