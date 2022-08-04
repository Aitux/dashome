using Dashome.Core.Extensions;
using Dashome.Core.Models;
using Dashome.Core.Models.Entities.Base;
using Dashome.Core.Repositories;
using Dashome.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Dashome.Infrastructure.Repositories.Base;

public class CrudRepository<TEntity> : CrudRepository<TEntity, Guid>, ICrudRepository<TEntity>
    where TEntity : class, IEntity<Guid>
{
    public CrudRepository(IUnitOfWork unitOfWork, ILogger<CrudRepository<TEntity>> logger) : base(unitOfWork,
        logger)
    {
    }
}

public class CrudRepository<TEntity, TKey> : Repository<TEntity, TKey>, ICrudRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CrudRepository<TEntity, TKey>> _logger;

    public CrudRepository(IUnitOfWork unitOfWork, ILogger<CrudRepository<TEntity, TKey>> logger) : base(
        unitOfWork, logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public virtual async Task<TEntity?> GetAsync(TKey id, bool tracking = false)
    {
        _logger.LogDebug("Retrieving entity {@EntityName} with id {@Id}", typeof(TEntity).Name, id);

        return await Query(tracking).FirstOrDefaultAsync(entity => id.Equals(entity.Id));
    }

    public virtual Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        _logger.LogDebug("Retrieving entity {@EntityName} with filter {@Filter}", typeof(TEntity).Name,
            filter.ToString());
        return Query().FirstOrDefaultAsync(filter);
    }

    public virtual IQueryable<TEntity> List(Expression<Func<TEntity, bool>>? filter = null,
        QueryParameters? parameters = null)
    {
        _logger.LogDebug(
            "Retrieving entities {@EntityName} with filter {@Filter} and queryParameters {@QueryParameters}",
            typeof(TEntity).Name, filter?.ToString(), parameters);
        return Query().Where(filter ?? (_ => true))
            .AddQueryParameters(parameters);
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        _logger.LogDebug("Inserting entity {@EntityName} with data: {@Entity}", typeof(TEntity).Name, entity);

        await Set.AddAsync(entity).ConfigureAwait(false);
    }

    public virtual async Task InsertListAsync(IEnumerable<TEntity> entities)
    {
        _logger.LogDebug("Inserting entities {@EntityName} with data: {@Entities}", typeof(TEntity).Name,
            entities);

        await Set.AddRangeAsync(entities).ConfigureAwait(false);
    }

    public virtual void Update(TEntity entity)
    {
        _logger.LogDebug("Updating entity {@EntityName} with data: {@Entity}", typeof(TEntity).Name, entity);

        Set.Update(entity);
    }

    public virtual void UpdateList(IEnumerable<TEntity> entities)
    {
        _logger.LogDebug("Updating entities {@EntityName} with data: {@Entities}", typeof(TEntity).Name,
            entities);

        Set.UpdateRange(entities);
    }

    public virtual void Delete(TEntity entity)
    {
        _logger.LogDebug("Deleting entity {@EntityName} with data: {@Entity}", typeof(TEntity).Name, entity);

        Set.Remove(entity);
    }

    public virtual async Task<bool> DeleteAsync(TKey id)
    {
        _logger.LogDebug("Deleting entity {@EntityName} with id: {@Id}", typeof(TEntity).Name, id);

        TEntity? entity = await Query().FirstOrDefaultAsync(e => id.Equals(e.Id)).ConfigureAwait(false);

        if (entity == null) return false;

        Set.Remove(entity);

        return true;
    }

    public virtual void DeleteList(Expression<Func<TEntity, bool>> filter)
    {
        _logger.LogDebug("Deleting entities {@EntityName} with filter: {@Filter}", typeof(TEntity).Name,
            filter);

        Set.RemoveRange(Query().Where(filter));
    }

    public virtual void DeleteList(IEnumerable<TEntity> entities)
    {
        _logger.LogDebug("Deleting entities {@EntityName} with data: {@Entities}", typeof(TEntity).Name,
            entities);

        Set.RemoveRange(entities);
    }

    public virtual void Patch(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
    {
        _logger.LogDebug("Patching entity {@EntityName} with data: {@Entity} and properties: {@Properties}",
            typeof(TEntity).Name, entity, properties.ToString());

        EntityEntry<TEntity> entry = _unitOfWork.GetEntry(entity);

        Set.Attach(entity);

        foreach (Expression<Func<TEntity, object>> property in properties)
        {
            entry.Property(property).IsModified = true;
        }
    }
}
