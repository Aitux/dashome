namespace Dashome.Auth.Jwt.Options;

public class JwtAuthOptions
{
    public string Secret { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
}