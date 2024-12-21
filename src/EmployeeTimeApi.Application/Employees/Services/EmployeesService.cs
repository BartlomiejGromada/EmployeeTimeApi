using EmployeeTimeApi.Application.Employees.ApiObjects;
using EmployeeTimeApi.Application.Employees.Dtos;
using EmployeeTimeApi.Application.Employees.Repositories;
using EmployeeTimeApi.Application.Shared;

namespace EmployeeTimeApi.Application.Employees.Services;

internal sealed class EmployeesService : IEmployeesService
{
    private readonly IEmployeesRepository _repository;
    public EmployeesService(IEmployeesRepository repository)
    {
        _repository = repository;
    }

    public Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }
    public Task<Paged<EmployeeDto>> GetPagedAsync(BrowseEmployeesQuery query, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddAsync(AddEmployeeDto dto, CancellationToken? cancellationToken = null)
    {
        var id = await _repository.AddAsync(dto, cancellationToken);

        return id;
    }

    public Task UpdateAsync(int id, UpdateEmployeeDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
