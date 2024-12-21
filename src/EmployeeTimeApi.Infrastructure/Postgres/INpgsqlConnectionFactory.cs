using Npgsql;

namespace EmployeeTimeApi.Infrastructure.Postgres;

internal interface INpgsqlConnectionFactory
{
    NpgsqlConnection CreateConnection();
}
