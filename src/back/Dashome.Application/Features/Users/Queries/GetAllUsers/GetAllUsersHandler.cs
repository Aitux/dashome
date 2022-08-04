using Dashome.Application.Constants;
using Dashome.Application.Utils;
using Dashome.Core.Models;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Dashome.Application.Features.Users.Queries.GetAllUsers;

[HttpGet(ApiRoutes.Users.Base)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class GetAllUsersHandler : Endpoint<GetAllUsersCommand>
{
    private readonly IAuthorizedUser _authorizedUser;

    public GetAllUsersHandler(IAuthorizedUser authorizedUser)
    {
        _authorizedUser = authorizedUser;
    }

    public override async Task HandleAsync(GetAllUsersCommand req, CancellationToken ct)
    {
        var email = _authorizedUser.GetEmail();

        await SendAsync(email ?? string.Empty, cancellation: ct);
    }
}