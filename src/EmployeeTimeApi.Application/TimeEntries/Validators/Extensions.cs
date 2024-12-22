using EmployeeTimeApi.Application.TimeEntries.Dtos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeTimeApi.Application.TimeEntries.Validators;

internal static class Extensions
{
    public static IServiceCollection AddTimeEntriesValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AddTimeEntryDto>, AddTimeEntryValidator>();
        services.AddTransient<IValidator<UpdateTimeEntryDto>, UpdateTimeEntryValidator>();

        return services;
    }
}