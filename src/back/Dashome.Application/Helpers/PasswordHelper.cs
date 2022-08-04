using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Dashome.Application.Helpers;

public static class PasswordHelper
{
    public static byte[] GetSecureSalt()
    {
        return RandomNumberGenerator.GetBytes(32);
    }

    public static string Hash(string password, byte[] salt)
    {
        byte[] derivedKey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 10000, 32);
        return Convert.ToBase64String(derivedKey);
    }
    
    public static bool CheckComplexity(string password)
    {
        return password.Length >= 8 && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit) && password.Any(IsSpecialCharacter);
    }
    
    private static bool IsSpecialCharacter(char c)
    {
        const string specialCharacters = @"!@#$%^&*()_+-={}[]|\;:'<>?,./`~";
        return specialCharacters.Contains(c);
    }
}