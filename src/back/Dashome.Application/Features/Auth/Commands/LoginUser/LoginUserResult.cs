namespace Dashome.Application.Features.Auth.Commands.LoginUser;

public class LoginUserResult
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}