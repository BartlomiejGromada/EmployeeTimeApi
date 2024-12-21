using EmployeeTimeApi.Application.Employees.Dtos;

namespace EmployeeTimeApi.Application.Employees.Repositories;

internal interface IEmployeesRepository
{
    Task<int> AddAsync(AddEmployeeDto dto, CancellationToken? cancellationToken = default);
}
