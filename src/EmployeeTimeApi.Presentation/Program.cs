
using EmployeeTimeApi.Application;
using EmployeeTimeApi.Infrastructure;
using EmployeeTimeApi.Shared.Infrastructure.Logger;

namespace EmployeeTimeApi.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.AddLogger(builder.Configuration);

        builder.Services.AddGeneralInfrastructure();

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();

        var app = builder.Build();

        app.UseGeneralInfrastructure(app.Environment);

        app.MapControllers();
        app.Run();
    }
}
