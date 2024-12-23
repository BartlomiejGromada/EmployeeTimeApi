using Dapper;
using EmployeeTimeApi.Infrastructure.Postgres;
using EmployeeTimeApi.Presentation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Testcontainers.PostgreSql;

namespace EmployeeTimeApi.Tests.Integration.Bases;

public class IntegrationTestWebAppFactory
    : WebApplicationFactory<Program>,
      IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:15-alpine")
            .WithDatabase("testdb")
            .WithUsername("testuser")
            .WithPassword("testpassword")
            .Build();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptorType =
                typeof(INpgsqlConnectionFactory);

            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == descriptorType);

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            var connectionString = _dbContainer.GetConnectionString();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                        { "postgres:connectionString", _dbContainer.GetConnectionString() }
                })
                .Build();

            services.AddScoped<INpgsqlConnectionFactory>(
                pr => new NpgsqlConnectionFactory(configuration));
        });
    }
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        await using var connection = new NpgsqlConnection(
            _dbContainer.GetConnectionString());
        await connection.ExecuteAsync(Sql);
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }

    private const string Sql = @"
                    CREATE SCHEMA IF NOT EXISTS workplace;
                    CREATE TABLE IF NOT EXISTS workplace.employees (
                        id SERIAL PRIMARY KEY,
                        first_name VARCHAR(100) NOT NULL,
                        last_name VARCHAR(100) NOT NULL,
                        email VARCHAR(150) UNIQUE NOT NULL
                    );
                    CREATE TABLE IF NOT EXISTS workplace.time_entries (
                        id SERIAL PRIMARY KEY,
                        employee_id INT NOT NULL,
                        date DATE NOT NULL,
                        hours_worked NUMERIC(5, 2) NOT NULL CHECK (
                            hours_worked >= 1
                            AND hours_worked <= 24
                        ),
                        CONSTRAINT fk_employee FOREIGN KEY (employee_id) REFERENCES workplace.employees(id) ON DELETE CASCADE
                    );
                    CREATE TABLE IF NOT EXISTS workplace.Account (
                        id SERIAL PRIMARY KEY,
                        email VARCHAR(150) UNIQUE NOT NULL,
                        hashed_password VARCHAR(255) NOT NULL,
                        role VARCHAR(50) NOT NULL
                    );
                    INSERT INTO workplace.account(email, hashed_password, role)
                    VALUES (
                            'admin@example.com',
                            'AQAAAAIAAYagAAAAEIWporczHhx4DCOoXDhuL33A4hqmNm8x+Cf7Tx8jPAmMxSCOHRD25BepETJ8QxeiHw==',
                            'admin'
                        );
                    INSERT INTO workplace.account(email, hashed_password, role)
                    VALUES (
                            'employee@example.com',
                            'AQAAAAIAAYagAAAAEIWporczHhx4DCOoXDhuL33A4hqmNm8x+Cf7Tx8jPAmMxSCOHRD25BepETJ8QxeiHw==',
                            'user'
                        );";
}