
using EmployeeTimeApi.Application.Shared;
using EmployeeTimeApi.Domain.TimeEntries.Models;

namespace EmployeeTimeApi.Application.TimeEntries.Repositories;

internal interface ITimeEntriesRepository
{
    Task<Paged<TimeEntry>> GetPagedAsync(
        int employeeId,
        int page,
        int results,
        CancellationToken? cancellationToken = default);
    Task<TimeEntry?> GetByIdAsync(
        int id,
        CancellationToken? cancellationToken = default);
    Task AddAsync(
        int employeeId,
        TimeEntry timeEntry,
        CancellationToken? cancellationToken = default);
    Task<bool> IsEntryAlreadyRegisteredOnDateAsync(
        int employeeId,
        DateTime date,
        int? entryId = null,
        CancellationToken? cancellationToken = default);
    Task DeleteByIdAsync(
        int id,
        CancellationToken? cancellationToken = default);
    Task UpdateAsync(
        int entryId,
        TimeEntry timeEntry,
        CancellationToken? cancellationToken = default);
}
