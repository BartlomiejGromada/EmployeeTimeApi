using EmployeeTimeApi.Application.Shared;
using EmployeeTimeApi.Application.TimeEntries.ApiObjects;
using EmployeeTimeApi.Application.TimeEntries.Dtos;

namespace EmployeeTimeApi.Application.TimeEntries.Services;

internal interface ITimeEntriesService
{
    Task<Paged<TimeEntryDto>> GetPagedAsync(
        int employeeId,
        BrowseTimeEntriesQuery browseQuery,
        CancellationToken? cancellationToken = default);
    Task AddAsync(
        int employeeId,
        AddTimeEntryDto dto,
        CancellationToken? cancellationToken = default);
    Task DeleteByIdAsync(
        int employeeId,
        int entryId, 
        CancellationToken? cancellationToken = default);
    Task UpdateAsync(
        int employeeId, 
        int entryId,
        UpdateTimeEntryDto dto,
        CancellationToken? cancellationToken = default);
}
