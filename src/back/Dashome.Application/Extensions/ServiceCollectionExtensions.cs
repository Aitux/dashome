using Dashome.Application.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Dashome.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}