namespace ContosoAcai.Common;

public static class StringUtils
{
    public static string NormalizeEmail(this string email)
    {
        if (string.IsNullOrEmpty(email))
            return string.Empty;

        var normalizedEmail = email
            .Trim()
            .ToLowerInvariant();

        return normalizedEmail;
    }
    
    public static string Truncate(this string value, int maxLength = 20)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        value = value.Trim();
        
        return value.Length <= maxLength ? value : value[..maxLength];
    }
    
    public static string CleanFileName(this string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return string.Empty;

        return fileName
            .Replace(" ", String.Empty)
            .ToLower()
            .Trim();
    }
}