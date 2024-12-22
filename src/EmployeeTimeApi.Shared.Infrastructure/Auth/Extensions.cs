using EmployeeTimeApi.Shared.Abstractions.Auth;
using EmployeeTimeApi.Shared.Abstractions.Contexts;
using EmployeeTimeApi.Shared.Infrastructure.Contexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace EmployeeTimeApi.Shared.Infrastructure.Auth;

public static class Extensions
{
    private const string AccessTokenCookieName = "__access-token";
    private const string AuthorizationHeader = "authorization";

    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        var options = services.GetOptions<AuthOptions>("auth");

        services.AddSingleton(new CookieOptions
        {
            HttpOnly = options.Cookie.HttpOnly,
            Secure = options.Cookie.Secure,
            SameSite = options.Cookie.SameSite?.ToLowerInvariant() switch
            {
                "strict" => SameSiteMode.Strict,
                "lax" => SameSiteMode.Lax,
                "none" => SameSiteMode.None,
                "unspecified" => SameSiteMode.Unspecified,
                _ => SameSiteMode.Unspecified
            }
        });

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = options.ValidIssuer,
            ValidateAudience = options.ValidateAudience,
            ValidateIssuer = options.ValidateIssuer,
            ValidateLifetime = options.ValidateLifetime,
            ClockSkew = TimeSpan.Zero
        };

        if (string.IsNullOrWhiteSpace(options.IssuerSigningKey))
        {
            throw new ArgumentException("Missing issuer signing key.", nameof(options.IssuerSigningKey));
        }

        var rawKey = Encoding.UTF8.GetBytes(options.IssuerSigningKey);
        tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(rawKey);

        services
            .AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = tokenValidationParameters;
            if (!string.IsNullOrWhiteSpace(options.Challenge))
            {
                o.Challenge = options.Challenge;
            }

            o.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (context.Request.Cookies.TryGetValue(AccessTokenCookieName, out var token))
                    {
                        context.Token = token;
                    }

                    return Task.CompletedTask;
                },
            };
        });

        services.AddSingleton(options);

        services.AddScoped<IAccountContext, AccountContext>();

        return services;
    }

    public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.Use(async (ctx, next) =>
        {
            if (ctx.Request.Headers.ContainsKey(AuthorizationHeader))
            {
                ctx.Request.Headers.Remove(AuthorizationHeader);
            }

            if (ctx.Request.Cookies.ContainsKey(AccessTokenCookieName))
            {
                var authenticateResult = await ctx.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                if (authenticateResult.Succeeded && authenticateResult.Principal is not null)
                {
                    ctx.User = authenticateResult.Principal;
                }
            }

            await next();
        });

        return app;
    }
}
