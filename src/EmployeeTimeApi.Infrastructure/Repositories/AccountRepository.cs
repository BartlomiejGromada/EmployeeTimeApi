using Dapper;
using EmployeeTimeApi.Application.Accounts.Repositories;
using EmployeeTimeApi.Domain.Accounts.Models;
using EmployeeTimeApi.Infrastructure.Postgres;

namespace EmployeeTimeApi.Infrastructure.Repositories;

internal sealed class AccountRepository : IAccountRepository
{
    private readonly INpgsqlConnectionFactory _connectionFactory;

    public AccountRepository(INpgsqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> IsExistAsync(
        string email,
        CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        const string query = @"
                SELECT EXISTS (
                    SELECT 1
                    FROM workplace.account
                    WHERE Email = @Email)";

        return await connection.ExecuteScalarAsync<bool>(query, new { Email = email });
    }

    public async Task<Account?> GetByEmailAsync(
    string email,
    CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        const string query = @"
                SELECT id, email, hashed_password, role
                FROM workplace.account
                WHERE email = @Email";

        return await connection.QueryFirstOrDefaultAsync<Account?>(query, new { Email = email });
    }

    public async Task AddAsync(
        Account account,
        CancellationToken? cancellationToken = default)
    {
        await using var connection = _connectionFactory.CreateConnection();

        const string query = @"
                    INSERT INTO workplace.account (email, hashed_password, role)
                    VALUES (@Email, @HashedPassword, @Role);
        ";

        await connection.ExecuteAsync(query, account);
    }
}