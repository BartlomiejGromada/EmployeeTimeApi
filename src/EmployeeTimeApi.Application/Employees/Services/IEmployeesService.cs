using EmployeeTimeApi.Application.Employees.ApiObjects;
using EmployeeTimeApi.Application.Employees.Dtos;
using EmployeeTimeApi.Application.Shared;

namespace EmployeeTimeApi.Application.Employees.Services;

internal interface IEmployeesService
{
    Task<Paged<EmployeeDto>> GetPagedAsync(
        BrowseEmployeesQuery query, 
        CancellationToken? cancellationToken = default);
    Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken? cancellationToken = default);
    Task<int> AddAsync(AddEmployeeDto dto, CancellationToken? cancellationToken = default);
    Task UpdateAsync(int id, UpdateEmployeeDto dto, CancellationToken cancellationToken);
    Task DeleteByIdAsync(int id, CancellationToken cancellationToken);
}
