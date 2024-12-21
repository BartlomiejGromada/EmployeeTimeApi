using Dapper;
using EmployeeTimeApi.Application.Employees.Dtos;
using EmployeeTimeApi.Application.Employees.Repositories;
using EmployeeTimeApi.Infrastructure.Postgres;

namespace EmployeeTimeApi.Infrastructure.Repositories;

internal sealed class EmployeesRepository : IEmployeesRepository
{
    private readonly INpgsqlConnectionFactory _connectionFactory;

    public EmployeesRepository(INpgsqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> AddAsync(AddEmployeeDto dto, CancellationToken? cancellationToken = null)
    {
        await using var connection = _connectionFactory.CreateConnection();

        var query = @"
            INSERT INTO workplace.employees (first_name, last_name, email)
            VALUES (@FirstName, @LastName, @Email)
            RETURNING id";

        var id = await connection.QuerySingleAsync<int>(query, dto);
        return id;
    }
}
