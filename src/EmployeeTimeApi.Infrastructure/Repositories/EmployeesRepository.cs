using Dapper;
using EmployeeTimeApi.Application.Employees.Repositories;
using EmployeeTimeApi.Application.Shared;
using EmployeeTimeApi.Domain.Employees.Models;
using EmployeeTimeApi.Infrastructure.Postgres;

namespace EmployeeTimeApi.Infrastructure.Repositories;

internal sealed class EmployeesRepository : IEmployeesRepository
{
    private readonly INpgsqlConnectionFactory _connectionFactory;

    public EmployeesRepository(INpgsqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> IsEmailAlreadyTakenAsync(
        string email,
        int? id = null,
        CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        var query = string.Empty;

        if(id is not null)
        {
            query = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM workplace.employees
                    WHERE Email = @Email AND Id != @Id)";
        }
        else
        {
            query = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM workplace.employees
                    WHERE Email = @Email)";
        }

        return await connection.ExecuteScalarAsync<bool>(query, new { Email = email, Id =  id });
    }

    public async Task<Paged<Employee>> GetPagedAsync(
        int page,
        int results,
        CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        var offset = (page - 1) * results;

        const string sql = @"
                SELECT id, first_name, last_name, email
                FROM workplace.employees
                ORDER BY id
                LIMIT @ResultsPerPage OFFSET @Offset;

                SELECT COUNT(*) FROM workplace.employees;
            ";

        using var multi = await connection.QueryMultipleAsync(sql, new { ResultsPerPage = results, Offset = offset });
        {
           var employees = multi.Read<Employee>().ToList();

           var totalResults = multi.Read<long>().Single();

           var totalPages = (int)Math.Ceiling((double)totalResults / results);

           return Paged<Employee>.Create(
                employees,
                page,
                results,
                totalPages,
                totalResults);
        }
    }

    public async Task<bool> IsExistByIdAsync(
        int id,
        CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        const string query = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM workplace.employees
                    WHERE Id = @Id)";

        return await connection.ExecuteScalarAsync<bool>(query, new { Id = id });
    }
   
    public async Task<Employee?> GetByIdAsync(
        int id,
        CancellationToken? cancellationToken)
    {
        await using var connection = _connectionFactory.CreateConnection();

        const string query = @"
                SELECT id, first_name, last_name, email
                FROM workplace.employees
                WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Employee>(query, new { Id = id });
    }

    public async Task<int> AddAsync(
        Employee employee,
        CancellationToken? cancellationToken = null)
    {
        await using var connection = _connectionFactory.CreateConnection();

        const string query = @"
            INSERT INTO workplace.employees (first_name, last_name, email)
            VALUES (@FirstName, @LastName, @Email)
            RETURNING id;";

        var id = await connection.QuerySingleAsync<int>(query, employee);
        return id;
    }

    public async Task UpdateAsync(
        int id,
        Employee employee,
        CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        const string query = @"
            UPDATE workplace.employees
            SET
                    first_name = @FirstName,
                    last_name = @LastName,
                    email = @Email
            WHERE
                    id = @Id";

        await connection.ExecuteAsync(query, new
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Id = id,
        });
    }
    
    public async Task DeleteByIdAsync(
        int id,
        CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        const string query = @"
                    DELETE FROM workplace.employees 
                    WHERE id = @Id;";

        await connection.ExecuteAsync(query, new { Id = id });
    }
}
