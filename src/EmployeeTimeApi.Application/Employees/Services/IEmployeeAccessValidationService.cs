using EmployeeTimeApi.Application.Employees.Dtos;

namespace EmployeeTimeApi.Application.Employees.Services;

internal interface IEmployeeAccessValidationService
{
    Task<EmployeeDto?> ValidateAsync(int employeeId, CancellationToken? cancellationToken = default);
}
