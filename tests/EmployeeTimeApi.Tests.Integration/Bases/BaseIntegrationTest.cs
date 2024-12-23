using EmployeeTimeApi.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace EmployeeTimeApi.Tests.Integration.Bases;

public abstract class BaseIntegrationTest
    : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly NpgsqlConnection DbConnection;
    protected readonly INpgsqlConnectionFactory _connectionFactory;

    protected BaseIntegrationTest(
        IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        _connectionFactory = _scope.ServiceProvider
            .GetRequiredService<INpgsqlConnectionFactory>();
    }

    public void Dispose()
    {
        _scope?.Dispose();
        DbConnection?.Dispose();
    }
}