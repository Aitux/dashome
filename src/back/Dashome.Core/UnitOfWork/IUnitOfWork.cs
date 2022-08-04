using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Dashome.Core.UnitOfWork;


public interface IUnitOfWork
{
    bool HasActiveTransaction();
    IDbContextTransaction GetCurrentTransaction();
    IExecutionStrategy CreateExecutionStrategy();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync(IDbContextTransaction transaction);
    void RollbackTransaction();
    void TrackEntity<TEntity>(TEntity entity);
    Task<int> CommitAsync();
    EntityEntry<TEntity> GetEntry<TEntity>(TEntity entity) where TEntity : class;
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    void ClearTracker();
    Task EnsureInitializedAsync();

    Task SetAzureSqlServerDatabasePricing(string edition = "standard", string maxSize = "2 GB",
        string serviceObjective = "S1");
}
