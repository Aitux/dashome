using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashome.Core.Models.Entities.Base;

public class Entity : Entity<Guid> {}

public class Entity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual TKey Id { get; set; }
}

public interface IEntity<out TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; }
}
