using Dapper;
using EmployeeTimeApi.Application.Shared;
using EmployeeTimeApi.Application.TimeEntries.Repositories;
using EmployeeTimeApi.Domain.Employees.Models;
using EmployeeTimeApi.Domain.TimeEntries.Models;
using EmployeeTimeApi.Infrastructure.Postgres;

namespace EmployeeTimeApi.Infrastructure.Repositories;

internal sealed class TimeEntriesRepository : ITimeEntriesRepository
{
    private readonly INpgsqlConnectionFactory _connectionFactory;

    public TimeEntriesRepository(INpgsqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Paged<TimeEntry>> GetPagedAsync(
        int employeeId,
        int page,
        int results,
        CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        var offset = (page - 1) * results;

        const string sql = @"
                SELECT id, date, hours_worked
                FROM workplace.time_entries
                WHERE employee_id = @EmployeeId
                ORDER BY date DESC
                LIMIT @ResultsPerPage OFFSET @Offset;

                SELECT COUNT(*) FROM workplace.time_entries
                WHERE employee_id = @EmployeeId;
            ";

        using var multi = await connection.QueryMultipleAsync(sql, 
            new
            { 
                    EmployeeId = employeeId, 
                    ResultsPerPage = results, 
                    Offset = offset 
            });
        {
            var timeEntries = multi.Read<TimeEntry>().ToList();

            var totalResults = multi.Read<long>().Single();

            var totalPages = (int)Math.Ceiling((double)totalResults / results);

            return Paged<TimeEntry>.Create(
                 timeEntries,
                 page,
                 results,
                 totalPages,
                 totalResults);
        }
    }

    public async Task<TimeEntry?> GetByIdAsync(
        int id,
        CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        const string query = @"
                SELECT id, employee_id, date, hours_worked
                FROM workplace.time_entries
                WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<TimeEntry>(query, new { Id = id });
    }

    public async Task AddAsync(
        int employeeId,
        TimeEntry timeEntry,
        CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        const string query = @"
            INSERT INTO workplace.time_entries (employee_id, date, hours_worked)
            VALUES (@EmployeeId, @Date, @HoursWorked);
        ";

        await connection.ExecuteAsync(query, new
        {
            EmployeeId = employeeId,
            Date = timeEntry.Date,
            HoursWorked = timeEntry.HoursWorked
        });
    }

    public async Task<bool> IsEntryAlreadyRegisteredOnDateAsync(
        int employeeId,
        DateTime date,
        int? entryId = null,
        CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        //const string query = @"
        //    SELECT EXISTS (
        //            SELECT 1
        //            FROM workplace.time_entries
        //            WHERE employee_id = @EmployeeId
        //            AND DATE(date) = @Date);
        //";

        const string query = @"
        SELECT EXISTS (
            SELECT 1
            FROM workplace.time_entries
            WHERE employee_id = @EmployeeId
              AND DATE(date) = @Date
              AND (@EntryId IS NULL OR id != @EntryId));
        ";

        return await connection.ExecuteScalarAsync<bool>(query, new
        {
            EmployeeId = employeeId,
            Date = date.Date,
            EntryId = entryId,
        });
    }
    public async Task DeleteByIdAsync(
        int id, 
        CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        const string query = @"
                    DELETE FROM workplace.time_entries 
                    WHERE id = @Id;";

        await connection.ExecuteScalarAsync<bool>(query, new { Id = id });
    }
   
    public async Task UpdateAsync(
        int entryId,
        TimeEntry timeEntry,
        CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        const string query = @"
            UPDATE workplace.time_entries
            SET
                    date = @Date,
                    hours_worked = @HoursWorked
            WHERE
                    id = @Id";

        await connection.ExecuteAsync(query, new
        {
            Date = timeEntry.Date,
            HoursWorked = timeEntry.HoursWorked,
            Id = entryId,
        });
    }
}
