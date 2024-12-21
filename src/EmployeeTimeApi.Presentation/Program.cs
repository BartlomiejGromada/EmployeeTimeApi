
using EmployeeTimeApi.Application;
using EmployeeTimeApi.Infrastructure;

namespace EmployeeTimeApi.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();
        builder.Services.AddGeneralInfrastructure();

        var app = builder.Build();

        app.UseGeneralInfrastructure(app.Environment);

        app.MapControllers();
        app.Run();
    }
}
