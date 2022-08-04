using Dashome.Application.Constants;
using Dashome.Application.Services;
using Dashome.Core.Repositories;
using Dashome.Domain.Models.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace Dashome.Application.Features.Auth.Commands.LoginUser;

public class LoginUserHandler : Endpoint<LoginUserCommand>
{
    private readonly ITokenService _tokenService;
    private readonly ICrudRepository<UserEntity> _userRepository;
    private readonly IPasswordHasher<UserEntity> _passwordHasher;

    public LoginUserHandler(ITokenService tokenService, ICrudRepository<UserEntity> userRepository, IPasswordHasher<UserEntity> passwordHasher)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes(ApiRoutes.Auth.Login);
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginUserCommand req, CancellationToken ct)
    {
        UserEntity? user = await _userRepository.GetAsync(x => x.Email == req.Email);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var passwordValidityResult = _passwordHasher.VerifyHashedPassword(user, user.Password, req.Password);

        switch (passwordValidityResult)
        {
            case PasswordVerificationResult.Success:
            {
                var result = new LoginUserResult
                {
                    Token = _tokenService.GetAccessToken(user),
                    RefreshToken = _tokenService.GetRefreshToken(user)
                };
        
                await SendAsync(result, cancellation: ct);
                break;
            }
            case PasswordVerificationResult.Failed:
                AddError("Invalid password");
                await SendErrorsAsync(cancellation: ct);
                break;
        }
    }
}