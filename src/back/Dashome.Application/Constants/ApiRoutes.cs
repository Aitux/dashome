namespace Dashome.Application.Constants;

public class ApiRoutes
{
    public class Auth
    {
        public const string Base = nameof(Auth);
        public const string Login = $"{Base}/login";
        public const string Register = $"{Base}/register";
        public const string Refresh = $"{Base}/refresh";
        public const string Me = $"{Base}/me";
    }
    
    public class Users
    {
        public const string Base = nameof(Users);
    }
}