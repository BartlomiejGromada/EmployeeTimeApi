using EmployeeTimeApi.Infrastructure.Configurations;
using EmployeeTimeApi.Shared;
using EmployeeTimeApi.Shared.Infrastructure;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace EmployeeTimeApi.Infrastructure.Postgres;

public sealed class NpgsqlConnectionFactory : INpgsqlConnectionFactory
{
    private const string PostgresOptionsName = "postgres";
    private readonly IConfiguration _configuration;

    public NpgsqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public NpgsqlConnection CreateConnection()
    {
        var options = _configuration.GetOptions<PostgresOptions>(PostgresOptionsName);

        return new NpgsqlConnection(options.ConnectionString);
    }
}
