using Dashome.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Dashome.Infrastructure.UnitOfWorks;

public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    internal IDbContextTransaction _currentTransaction;
    private readonly TContext _context;
    private readonly IConfiguration _configuration;

    public UnitOfWork(IConfiguration configuration, TContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public bool HasActiveTransaction() => _currentTransaction != null;

    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

    public IExecutionStrategy CreateExecutionStrategy() => _context.Database.CreateExecutionStrategy();

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null)
            return null;

        _currentTransaction = await _context.Database.BeginTransactionAsync();

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        CheckTransaction(transaction);

        try
        {
            await CommitAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    private void CheckTransaction(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction)
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
    }

    public virtual void TrackEntity<TEntity>(TEntity entity) =>
        _context.ChangeTracker.TrackGraph(entity,
            e => { e.Entry.State = e.Entry.IsKeySet ? EntityState.Modified : EntityState.Added; });

    public Task<int> CommitAsync() => _context.SaveChangesAsync();

    public virtual EntityEntry<TEntity> GetEntry<TEntity>(TEntity entity) where TEntity : class =>
        _context.Entry(entity);

    public DbSet<TEntity> Set<TEntity>() where TEntity : class => _context.Set<TEntity>();

    public void ClearTracker() => _context.ChangeTracker.Clear();

    public async Task EnsureInitializedAsync()
    {
        bool allMigrationsApplied = await AreAllMigrationsApplied();

        if (allMigrationsApplied)
        {
            return;
        }

        await _context.Database.MigrateAsync();

        if (_configuration.GetValue<bool>("IsAzureDb"))
        {
            await SetAzureSqlServerDatabasePricing();
        }
    }

    public async Task SetAzureSqlServerDatabasePricing(string edition = "standard", string maxSize = "2 GB",
        string serviceObjective = "S1")
    {
        try
        {
            string databaseName = _context.Database.GetDbConnection().Database;
            await _context.Database.ExecuteSqlRawAsync(
                $"ALTER DATABASE [{databaseName}] MODIFY (EDITION = '{edition}', MAXSIZE = {maxSize}, SERVICE_OBJECTIVE = '{serviceObjective}')");
        }
        catch (Exception)
        {
            //
        }
    }

    private async Task<bool> AreAllMigrationsApplied()
    {
        try
        {
            IEnumerable<string> pendingMigrations = await _context.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                return false;
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
