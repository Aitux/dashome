using Dashome.Core.Repositories;
using Dashome.Core.UnitOfWork;
using Dashome.Infrastructure.EF;
using Dashome.Infrastructure.Initializers;
using Dashome.Infrastructure.Repositories.Base;
using Dashome.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dashome.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(builder =>
        {
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();

        services.AddAsyncInitializer<DbInitializer>();
        
        return services;
    }
}