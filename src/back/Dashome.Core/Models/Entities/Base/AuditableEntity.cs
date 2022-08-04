using System.ComponentModel.DataAnnotations;

namespace Dashome.Core.Models.Entities.Base;

public abstract class AuditableEntity : AuditableEntity<Guid>
{
}

public abstract class AuditableEntity<TKey> : Entity<TKey> where TKey : IEquatable<TKey>
{
    public DateTimeOffset CreatedAt { get; set; }
    [StringLength(255)]
    public TKey CreatedBy { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    [StringLength(255)]
    public TKey UpdatedBy { get; set; }
}
