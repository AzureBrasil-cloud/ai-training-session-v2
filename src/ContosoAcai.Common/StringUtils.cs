namespace ContosoAcai.Common;

public static class StringUtils
{
    private static readonly HashSet<string> AllowedImageExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".png",
        ".jpg"
    };
    
    private static readonly HashSet<string> AllowedAudioExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".mp3"
    };
    
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
    
    public static bool IsValidFileExtension(this string extension)
    {
        return !string.IsNullOrWhiteSpace(extension) && AllowedImageExtensions.Contains(extension);
    }
    
    public static bool IsValidAudioExtension(this string extension)
    {
        return !string.IsNullOrWhiteSpace(extension) && AllowedAudioExtensions.Contains(extension);
    }
}