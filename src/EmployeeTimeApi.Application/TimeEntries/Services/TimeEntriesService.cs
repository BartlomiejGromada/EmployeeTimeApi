using AutoMapper;
using EmployeeTimeApi.Application.Employees.Repositories;
using EmployeeTimeApi.Application.Shared;
using EmployeeTimeApi.Application.TimeEntries.ApiObjects;
using EmployeeTimeApi.Application.TimeEntries.Dtos;
using EmployeeTimeApi.Application.TimeEntries.Repositories;
using EmployeeTimeApi.Domain.Employees.Exceptions;
using EmployeeTimeApi.Domain.TimeEntries.Exceptions;
using EmployeeTimeApi.Domain.TimeEntries.Models;

namespace EmployeeTimeApi.Application.TimeEntries.Services;

internal sealed class TimeEntriesService : ITimeEntriesService
{
    private readonly ITimeEntriesRepository _timeEntriesRepository;
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IMapper _mapper;

    public TimeEntriesService(
        ITimeEntriesRepository repository,
        IEmployeesRepository employeesRepository,
        IMapper mapper)
    {
        _timeEntriesRepository = repository;
        _employeesRepository = employeesRepository;
        _mapper = mapper;
    }

    public async Task<Paged<TimeEntryDto>> GetPagedAsync(
        int employeeId,
        BrowseTimeEntriesQuery query,
        CancellationToken? cancellationToken = default)
    {
        var isEmployeeExist = await _employeesRepository.IsExistByIdAsync(employeeId, cancellationToken);

        if (!isEmployeeExist)
        {
            throw new EmployeeDoesntExistException(employeeId);
        }

        var timeEntries = await _timeEntriesRepository.GetPagedAsync(employeeId, query.Page, query.Results, cancellationToken);

        return timeEntries.Map(_mapper.Map<TimeEntryDto>);
    }

    public async Task AddAsync(
        int employeeId, 
        AddTimeEntryDto dto, 
        CancellationToken? cancellationToken = null)
    {
        var isEmployeeExist = await _employeesRepository.IsExistByIdAsync(employeeId, cancellationToken);

        if (!isEmployeeExist)
        {
            throw new EmployeeDoesntExistException(employeeId);
        }

        var isEntryAlreadyRegisteredOnDate = await _timeEntriesRepository.IsEntryAlreadyRegisteredOnDateAsync(
            employeeId, 
            dto.Date,
            entryId: null,
            cancellationToken);

        if (isEntryAlreadyRegisteredOnDate)
        {
            throw new TimeEntryAlreadyExistsOnDateException(employeeId, dto.Date);
        }

        if (dto.HoursWorked < 1 | dto.HoursWorked > 24)
        {
            throw new InvalidHoursWorkedException();
        }

        var timeEntry = _mapper.Map<TimeEntry>(dto);
            
        await _timeEntriesRepository.AddAsync(employeeId, timeEntry, cancellationToken);
    }

    public async Task UpdateAsync(
        int employeeId,
        int entryId,
        UpdateTimeEntryDto dto,
        CancellationToken? cancellationToken = default)
    {
        var isEmployeeExist = await _employeesRepository.IsExistByIdAsync(employeeId, cancellationToken);

        if (!isEmployeeExist)
        {
            throw new EmployeeDoesntExistException(employeeId);
        }

        var timeEntry = await _timeEntriesRepository.GetByIdAsync(entryId, cancellationToken);

        if (timeEntry is null)
        {
            throw new TimeEntryDoesntExistException(entryId);
        }

        if (timeEntry.EmployeeId != employeeId)
        {
            throw new EmployeeTimeEntryMismatchException(employeeId, entryId);
        }

        var isEntryAlreadyRegisteredOnDate = await _timeEntriesRepository.IsEntryAlreadyRegisteredOnDateAsync(
            employeeId,
            dto.Date,
            entryId,
            cancellationToken);

        if (isEntryAlreadyRegisteredOnDate)
        {
            throw new TimeEntryAlreadyExistsOnDateException(employeeId, dto.Date);
        }

        if(dto.HoursWorked < 1 | dto.HoursWorked > 24)
        {
            throw new InvalidHoursWorkedException();
        }

        var timeEntryToAdd = _mapper.Map<TimeEntry>(dto);

        await _timeEntriesRepository.UpdateAsync(entryId, timeEntryToAdd, cancellationToken);
    }

    public async Task DeleteByIdAsync(
        int employeeId,
        int entryId, 
        CancellationToken? cancellationToken = null)
    {
        var isEmployeeExist = await _employeesRepository.IsExistByIdAsync(employeeId, cancellationToken);

        if (!isEmployeeExist)
        {
            throw new EmployeeDoesntExistException(employeeId);
        }

        var timeEntry = await _timeEntriesRepository.GetByIdAsync(entryId, cancellationToken);

        if(timeEntry is null)
        {
            throw new TimeEntryDoesntExistException(entryId);
        }

        if (timeEntry.EmployeeId != employeeId)
        {
            throw new EmployeeTimeEntryMismatchException(employeeId, entryId);
        }

        await _timeEntriesRepository.DeleteByIdAsync(entryId, cancellationToken);
    }
}
