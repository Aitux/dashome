using Dashome.Core.Models.Entities.Base;

namespace Dashome.Domain.Models.Entities;

public class UserEntity : AuditableEntity
{
    public string Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public bool IsAdmin { get; set; }
}