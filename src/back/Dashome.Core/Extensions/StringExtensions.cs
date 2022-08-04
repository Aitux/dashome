namespace Dashome.Core.Extensions;

public static class StringExtensions
{
    public static string ToPascalCase(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return value;

        char first = char.ToUpper(value[0]);
        return value.Length == 1 ? first.ToString() : $"{first}{value[1..]}";
    }
}
