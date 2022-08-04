using Microsoft.Extensions.DependencyInjection;

namespace Dashome.Auth.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiAuth(this IServiceCollection services)
    {
        return services;
    }
}