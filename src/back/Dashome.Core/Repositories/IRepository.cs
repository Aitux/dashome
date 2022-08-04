using Dashome.Core.Models.Entities.Base;
using System.Linq.Expressions;

namespace Dashome.Core.Repositories;

public interface IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
{
    IQueryable<TEntity> Query(bool track = false);

    Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null);
}
