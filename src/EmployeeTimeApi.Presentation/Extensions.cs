using EmployeeTimeApi.Shared.Infrastructure.Api;
using EmployeeTimeApi.Shared.Infrastructure.Exceptions;
using FluentValidation.AspNetCore;
using FluentValidation;
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


        services.AddAuthorization();
        services.AddControllers().ConfigureApplicationPartManager(manager =>
        {
            manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
        });

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

        app.UseHttpsRedirection();
        app.UseAuthorization();

        return app;
    }
}
