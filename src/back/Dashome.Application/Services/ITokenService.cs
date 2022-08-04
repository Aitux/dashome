using Dashome.Domain.Models.Entities;

namespace Dashome.Application.Services;

public interface ITokenService
{
    string GetAccessToken(UserEntity user);
    string GetRefreshToken(UserEntity user);
}