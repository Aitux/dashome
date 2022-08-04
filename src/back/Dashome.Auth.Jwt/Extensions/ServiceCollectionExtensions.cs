using Dashome.Application.Services;
using Dashome.Auth.Jwt.Options;
using Dashome.Auth.Jwt.Services;
using Dashome.Core.Extensions;
using Dashome.Domain.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Dashome.Auth.Jwt.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITokenService, TokenService>();

        services.AddTransient<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
        
        services.ConfigureAndValidateSingleton<JwtAuthOptions>(configuration);
        return services;
    }
    
    public static AuthenticationBuilder AddJwtAuth(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        builder.AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Issuer"],
                ValidAudience = configuration["Issuer"],
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Secret"]))
            };
        });
        
        return builder;
    }
}