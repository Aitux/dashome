using Dashome.Application.Constants;
using Dashome.Application.Utils;
using Dashome.Core.Repositories;
using Dashome.Domain.Models.Dtos;
using Dashome.Domain.Models.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Dashome.Application.Features.Auth.Queries.GetMe;

[HttpGet(ApiRoutes.Auth.Me)]
[Authorize]
public class GetMeHandler : Endpoint<GetMeQuery, GetMeResult>
{
    private readonly ICrudRepository<UserEntity> _userRepository;
    private readonly IAuthorizedUser _authorizedUser;

    public GetMeHandler(ICrudRepository<UserEntity> userRepository, IAuthorizedUser authorizedUser)
    {
        _userRepository = userRepository;
        _authorizedUser = authorizedUser;
    }

    public override async Task HandleAsync(GetMeQuery req, CancellationToken ct)
    {
        var user = await _userRepository.GetAsync(x => x.Email == _authorizedUser.GetEmail());
        
        if (user == null)
        {
            AddError("User not found");
            await SendErrorsAsync(404, ct);
            return;
        }

        var result = new GetMeResult
        {
            User = new UserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Username
            }
        };
        
        await SendAsync(result, cancellation: ct);
    }
}