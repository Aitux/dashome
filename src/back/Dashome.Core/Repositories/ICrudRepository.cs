using Dashome.Core.Models;
using Dashome.Core.Models.Entities.Base;
using System.Linq.Expressions;

namespace Dashome.Core.Repositories;

public interface ICrudRepository<TEntity> : ICrudRepository<TEntity, Guid>
    where TEntity : class, IEntity<Guid>
{
}

public interface ICrudRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
{
    Task<TEntity?> GetAsync(TKey id, bool tracking = false);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter);

    IQueryable<TEntity> List(Expression<Func<TEntity, bool>>? filter = null,
        QueryParameters? parameters = null);

    Task InsertAsync(TEntity entity);

    Task InsertListAsync(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void UpdateList(IEnumerable<TEntity> entities);

    void Delete(TEntity entity);

    Task<bool> DeleteAsync(TKey id);

    void DeleteList(Expression<Func<TEntity, bool>> filter);

    void DeleteList(IEnumerable<TEntity> entities);

    void Patch(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
}