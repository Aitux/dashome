using Dashome.Application.Utils;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Dashome.Auth.Utils;

public class AuthorizedUser : IAuthorizedUser
{
    private readonly ClaimsPrincipal? _claimsPrincipal;

    public AuthorizedUser(IHttpContextAccessor httpContextAccessor)
    {
        _claimsPrincipal = httpContextAccessor.HttpContext?.User;
    }

    public string? GetEmail()
    {
        return _claimsPrincipal?.FindFirstValue(ClaimTypes.Email);
    }

    public Guid? GetUserId()
    {
        string? value = _claimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier);
        return value != null ? Guid.Parse(value) : null;
    }
}