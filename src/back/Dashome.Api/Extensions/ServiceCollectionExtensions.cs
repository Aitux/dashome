using Dashome.Application.Extensions;
using Dashome.Application.Utils;
using Dashome.Auth.Extensions;
using Dashome.Auth.Jwt.Extensions;
using Dashome.Auth.Oidc.Extensions;
using Dashome.Auth.Utils;
using Dashome.Core.Extensions;
using Dashome.Infrastructure.Extensions;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Dashome.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddFastEndpoints();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAsyncInitialization();
        services.AddHttpContextAccessor();
        services.AddCustomServices();

        services.AddJwtAuthServices(configuration.GetSection("Auth:Internal"));

        services.AddAuthentication()
            // .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            // {
            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuer = true,
            //         ValidateAudience = true,
            //         ValidateLifetime = true,
            //         ValidateIssuerSigningKey = true,
            //         ValidIssuer = configuration["Auth:Internal:Issuer"],
            //         ValidAudience = configuration["Auth:Internal:Issuer"],
            //         IssuerSigningKey =
            //             new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Internal:Secret"]))
            //     };
            // })
            .AddJwtBearer("External", options =>
            {
                options.Authority = configuration["Auth:External:Authority"];
                options.Audience = configuration["Auth:External:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(
                    // JwtBearerDefaults.AuthenticationScheme,
                    "External"
                )
                .RequireAuthenticatedUser()
                .Build();
        });
        services.AddApplication();
        services.AddCore();
        services.AddInfra(configuration.GetConnectionString("ApplicationDbContext"));
        return services;
    }

    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizedUser, AuthorizedUser>();
        return services;
    }
}