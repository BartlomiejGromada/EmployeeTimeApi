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
    private readonly IEmployeeAccessValidationService _accessService;
    public EmployeesService(
        IEmployeesRepository repository,
        IMapper mapper,
        IEmployeeAccessValidationService accessService)
    {
        _repository = repository;
        _mapper = mapper;
        _accessService = accessService;
    }

    public async Task<Paged<EmployeeDto>> GetPagedAsync(
        BrowseEmployeesQuery query,
        CancellationToken? cancellationToken = default)
    {
        var employees = await _repository.GetPagedAsync(query.Page, query.Results, cancellationToken);

        return employees.Map(_mapper.Map<EmployeeDto>);
    }

    public async Task<EmployeeDto?> GetByIdAsync(
        int id,
        CancellationToken? cancellationToken = default)
    {
        var employeeDto = await _accessService.ValidateAsync(id, cancellationToken);
        return employeeDto;
    }

    public async Task<int> AddAsync(
        AddEmployeeDto dto,
        CancellationToken? cancellationToken = default)
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

    public async Task UpdateAsync(
        int id,
        UpdateEmployeeDto dto,
        CancellationToken? cancellationToken = default)
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

    public async Task DeleteByIdAsync(
        int id,
        CancellationToken? cancellationToken = default)
    {
        var isExist = await _repository.IsExistByIdAsync(id, cancellationToken);

        if(!isExist)
        {
            throw new EmployeeDoesntExistException(id);
        }

        await _repository.DeleteByIdAsync(id, cancellationToken);
    }
}
