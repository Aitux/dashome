using Microsoft.Extensions.DependencyInjection;

namespace Dashome.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services;
    }
}