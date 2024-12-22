using AutoMapper;
using EmployeeTimeApi.Application.Employees.ApiObjects;
using EmployeeTimeApi.Application.Employees.Dtos;
using EmployeeTimeApi.Application.Employees.Repositories;
using EmployeeTimeApi.Application.Shared;
using EmployeeTimeApi.Domain.Employees.Exceptions;
using EmployeeTimeApi.Domain.Employees.Models;

namespace EmployeeTimeApi.Application.Employees.Services;

internal sealed class EmployeesService : IEmployeesService
{
    private readonly IEmployeesRepository _repository;
    private readonly IMapper _mapper;
    public EmployeesService(IEmployeesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Paged<EmployeeDto>> GetPagedAsync(BrowseEmployeesQuery query, CancellationToken? cancellationToken = null)
    {
        var employees = await _repository.GetPaged(query.Page, query.Results, cancellationToken);

        return employees.Map(_mapper.Map<EmployeeDto>);
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken? cancellationToken = null)
    {
        var employee = await _repository.GetByIdAsync(id, cancellationToken);

        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<int> AddAsync(AddEmployeeDto dto, CancellationToken? cancellationToken = null)
    {
        var ifAlreadyExist = await _repository.IsEmailAlreadyTakenAsync(dto.Email, id: null, cancellationToken);

        if (ifAlreadyExist)
        {
            throw new EmailAlreadyTakenException(dto.Email);
        }

        var employee = _mapper.Map<Employee>(dto);
        var id = await _repository.AddAsync(employee, cancellationToken);

        return id;
    }

    public async Task UpdateAsync(int id, UpdateEmployeeDto dto, CancellationToken cancellationToken)
    {
        var isExist = await _repository.IsExistByIdAsync(id, cancellationToken);
        
        if(!isExist)
        {
            throw new EmployeeDoesntExistException(id);
        }

        var isEmailAlreadyTaken = await _repository.IsEmailAlreadyTakenAsync(dto.Email, id, cancellationToken);

        if(isEmailAlreadyTaken)
        {
            throw new EmailAlreadyTakenException(dto.Email);
        }

        var employee = _mapper.Map<Employee>(dto);

        await _repository.UpdateAsync(id, employee, cancellationToken);
    }

    public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken)
    {
        var isExist = await _repository.IsExistByIdAsync(id, cancellationToken);

        if(!isExist)
        {
            throw new EmployeeDoesntExistException(id);
        }

        await _repository.DeleteByIdAsync(id, cancellationToken);
    }
}
