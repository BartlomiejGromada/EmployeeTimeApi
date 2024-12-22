using EmployeeTimeApi.Application.Employees.Dtos;
using EmployeeTimeApi.Application.Shared;
using EmployeeTimeApi.Domain.Employees.Models;

namespace EmployeeTimeApi.Application.Employees.Repositories;

internal interface IEmployeesRepository
{
    Task<bool> IsEmailAlreadyTakenAsync(string email, int? id = null, CancellationToken? cancellationToken = default);
    Task<bool> IsExistByIdAsync(int id, CancellationToken? cancellationToken = default);
    Task<Paged<Employee>> GetPaged(int page, int results, CancellationToken? cancellationToken = default);
    Task<Employee?> GetByIdAsync(int id, CancellationToken? cancellationToken);
    Task<int> AddAsync(Employee employee, CancellationToken? cancellationToken = default);
    Task UpdateAsync(int id, Employee employee, CancellationToken? cancellationToken = default);
    Task DeleteByIdAsync(int id, CancellationToken? cancellationToken = default);
}
