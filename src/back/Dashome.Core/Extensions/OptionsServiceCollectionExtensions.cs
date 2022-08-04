using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Dashome.Core.Extensions;

public static class OptionsServiceCollectionExtensions
{
    /// <summary>
    ///     Registers <see cref="IOptions{TOptions}" /> and <typeparamref name="TOptions" /> to the services container.
    ///     Also runs data annotation validation.
    /// </summary>
    /// <typeparam name="TOptions">The type of the options.</typeparam>
    /// <param name="services">The services collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The same services collection.</returns>
    public static IServiceCollection ConfigureAndValidateSingleton<TOptions>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TOptions : class, new()
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

        services
            .AddOptions<TOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations();
        return services.AddSingleton(x => x.GetRequiredService<IOptions<TOptions>>().Value);
    }
}
