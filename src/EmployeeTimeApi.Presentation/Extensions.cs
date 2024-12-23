using EmployeeTimeApi.Shared.Infrastructure.Api;
using EmployeeTimeApi.Shared.Infrastructure.Auth;
using EmployeeTimeApi.Shared.Infrastructure.Contexts;
using EmployeeTimeApi.Shared.Infrastructure.Exceptions;
using Microsoft.OpenApi.Models;

namespace EmployeeTimeApi.Presentation;

internal static class Extensions
{
    public static IServiceCollection AddGeneralInfrastructure(this IServiceCollection services)
    {
        services.AddErrorHandling();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Employee Time Management API",
                Version = "v1"
            });
        });


        services.AddControllers().ConfigureApplicationPartManager(manager =>
        {
            manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
        });

        services.AddAuth();
        services.AddAuthorization();
        services.AddContext();

        return services;
    }

    public static IApplicationBuilder UseGeneralInfrastructure(this IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseErrorHandling();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Time API");
            });
        }

        app.UseAuth();
        app.UseAuthorization();

        return app;
    }
}
