using Npgsql;

namespace EmployeeTimeApi.Infrastructure.Postgres;

public interface INpgsqlConnectionFactory
{
    NpgsqlConnection CreateConnection();
}
