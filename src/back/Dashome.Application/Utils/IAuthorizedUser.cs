namespace Dashome.Application.Utils;

public interface IAuthorizedUser
{
    public string? GetEmail();
    Guid? GetUserId();
}
