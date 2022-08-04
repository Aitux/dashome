using Dashome.Auth.Oidc.Options;
using Dashome.Core.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Dashome.Auth.Oidc.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOidcAuthServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureAndValidateSingleton<OidcAuthOptions>(configuration.GetSection("Auth:Oidc"));


        return services;
    }

    public static AuthenticationBuilder AddOidcAuth(this AuthenticationBuilder builder,
        IConfiguration configuration)
    {
        builder.AddOpenIdConnect(options =>
        {
            options.Authority = configuration.GetValue<string>("Auth:Oidc:Authority");
            options.ClientId = configuration.GetValue<string>("Auth:Oidc:ClientId");
            options.ClientSecret = configuration.GetValue<string>("Auth:Oidc:ClientSecret");
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.Scope.Clear();
            options.Scope.Add("openid");
            options.CallbackPath = new PathString("/callback");
            options.ClaimsIssuer = "Auth0";
        });

        return builder;
    }
}