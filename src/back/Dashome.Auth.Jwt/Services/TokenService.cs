using Dashome.Application.Services;
using Dashome.Auth.Jwt.Options;
using Dashome.Domain.Models.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dashome.Auth.Jwt.Services;

public class TokenService : ITokenService
{
    private readonly JwtAuthOptions _options;

    public TokenService(IOptions<JwtAuthOptions> options)
    {
        _options = options.Value;
    }

    public string GetAccessToken(UserEntity user)
    {
        return GenerateToken(new[] { new Claim("email", user.Email) }, false);
    }

    public string GetRefreshToken(UserEntity user)
    {
        return GenerateToken(new[] { new Claim("email", user.Email) }, true, TimeSpan.FromDays(365));
    }

    private string GenerateToken(IEnumerable<Claim> claims, bool refresh, TimeSpan? lifetime = null)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Secret);

        if (refresh)
            claims = claims.Concat(new[] { new Claim("refresh", bool.TrueString) });

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = lifetime.HasValue ? DateTime.Now.Add(lifetime.Value) : DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            Audience = _options.Audience,
            Issuer = _options.Issuer
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}