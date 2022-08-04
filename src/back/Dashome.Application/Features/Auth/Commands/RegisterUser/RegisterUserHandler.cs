using Dashome.Application.Constants;
using Dashome.Application.Helpers;
using Dashome.Core.Repositories;
using Dashome.Core.UnitOfWork;
using Dashome.Domain.Models.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace Dashome.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserHandler : Endpoint<RegisterUserCommand>
{
    private readonly ICrudRepository<UserEntity> _userRepository;
    private readonly IPasswordHasher<UserEntity> _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserHandler(ICrudRepository<UserEntity> userRepository, IPasswordHasher<UserEntity> passwordHasher, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes(ApiRoutes.Auth.Register);
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterUserCommand req, CancellationToken ct)
    {
        var existingUser = await _userRepository.GetAsync(x => x.Email == req.Email);
        
        if(existingUser != null)
        {
            ThrowError("Email already exists");
        }
        
        if(req.Password != req.ConfirmPassword)
        {
            ThrowError("Passwords do not match");
        }

        if (!PasswordHelper.CheckComplexity(req.Password))
        {
            ThrowError("Password is not complex enough");
        }
        
        var user = new UserEntity
        {
            Email = req.Email,
            Password = _passwordHasher.HashPassword(null, req.Password),
            Username = req.Email,
            FirstName = req.FirstName,
            LastName = req.LastName,
            IsActive = true,
            IsAdmin = false,
        };

        await _userRepository.InsertAsync(user);
        await _unitOfWork.CommitAsync();

        await SendAsync(new { user.Email }, cancellation: ct);
    }
}