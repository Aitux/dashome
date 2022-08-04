using Dashome.Application.Utils;
using Dashome.Core.Models.Entities.Base;
using Dashome.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dashome.Infrastructure.EF;

public class ApplicationDbContext : DbContext
{
    private readonly IAuthorizedUser _authorizedUser;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthorizedUser authorizedUser) : base(options)
    {
        _authorizedUser = authorizedUser;
    }

    public DbSet<UserEntity> Users { get; set; }
    
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    private void OnBeforeSaving()
    {
        foreach (EntityEntry entry in ChangeTracker.Entries())
        {
            if (entry.Entity is AuditableEntity entity)
            {
                var system = default (Guid);
                Guid? userId = _authorizedUser.GetUserId();

                switch (entry.State)
                {
                    case EntityState.Modified:
                        entity.UpdatedBy = userId ?? system;
                        entity.UpdatedAt = DateTimeOffset.UtcNow;
                        entry.Property(nameof(entity.CreatedAt)).IsModified = false;
                        entry.Property(nameof(entity.CreatedBy)).IsModified = false;
                        break;
                    case EntityState.Added:
                        entity.CreatedBy = userId ?? system;
                        entity.CreatedAt = DateTimeOffset.UtcNow;
                        break;
                }
            }
        }
    }
}