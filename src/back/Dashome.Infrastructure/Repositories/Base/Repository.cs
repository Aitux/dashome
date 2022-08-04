using Dashome.Core.Models.Entities.Base;
using Dashome.Core.Repositories;
using Dashome.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Dashome.Infrastructure.Repositories.Base;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    private readonly ILogger<Repository<TEntity, TKey>> _logger;
    protected readonly DbSet<TEntity> Set;

    public Repository(IUnitOfWork unitOfWork, ILogger<Repository<TEntity, TKey>> logger)
    {
        _logger = logger;
        Set = unitOfWork.Set<TEntity>();
    }

    public IQueryable<TEntity> Query(bool track = false) => track ? Set : Set.AsNoTracking();

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        _logger.LogDebug("Counting entity {@EntityName} with filter {@Filter}", typeof(TEntity).Name,
            filter?.ToString());

        return Query().Where(filter ?? (_ => true)).CountAsync();
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        _logger.LogDebug("Checking if an entity {@EntityName} exists with filter {@Filter}", typeof(TEntity).Name,
            filter?.ToString());

        return Query().Where(filter ?? (_ => true)).AnyAsync();
    }
}
