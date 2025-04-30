using System.Text.RegularExpressions;

namespace PowerPilotChat.Common;

public static class StringUtils
{
    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".c",
        ".cs",
        ".cpp",
        ".doc",
        ".docx",
        ".html",
        ".java",
        ".json",
        ".md",
        ".pdf",
        ".php",
        ".pptx",
        ".py",
        ".rb",
        ".tex",
        ".txt",
        ".css",
        ".js",
        ".sh",
        ".ts"
    };
    
    private static readonly Regex _emailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);
    
    public static string CleanPersonalIdentifier(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var cleanedValue = value
            .Replace(".", "")
            .Replace("-", "")
            .Replace("/", "")
            .Trim();

        return cleanedValue;
    }
    
    public static bool IsValidPersonalIdentifier(string personalIdentifier)
    {
        if (string.IsNullOrWhiteSpace(personalIdentifier))
        {
            return false;
        }

        personalIdentifier = personalIdentifier
            .Replace(".", string.Empty)
            .Replace("-", string.Empty)
            .Replace("/", string.Empty);
        
        return IsValidCnpj(personalIdentifier) || IsValidCpf(personalIdentifier);
    }
    
    public static bool IsValidCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
        {
            return false;
        }

        cnpj = cnpj
            .Replace(".", string.Empty)
            .Replace("-", string.Empty)
            .Replace("/", string.Empty);
        
        int[] mult1 = new int[12] {5,4,3,2,9,8,7,6,5,4,3,2};
        int[] mult2 = new int[13] {6,5,4,3,2,9,8,7,6,5,4,3,2};
        int sum;
        int rest;
        string digit;
        string hasCnpj;
        cnpj = cnpj.Trim();
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
        if (cnpj.Length != 14)
            return false;
        hasCnpj = cnpj.Substring(0, 12);
        sum = 0;
        for(int i=0; i<12; i++)
            sum += int.Parse(hasCnpj[i].ToString()) * mult1[i];
        rest = (sum % 11);
        if ( rest < 2)
            rest = 0;
        else
            rest = 11 - rest;
        digit = rest.ToString();
        hasCnpj = hasCnpj + digit;
        sum = 0;
        for (int i = 0; i < 13; i++)
            sum += int.Parse(hasCnpj[i].ToString()) * mult2[i];
        rest = (sum % 11);
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;
        digit = digit + rest.ToString();
        return cnpj.EndsWith(digit);
    }
    
    public static bool IsValidCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            return false;
        }
        
        cpf = cpf
            .Replace(".", string.Empty)
            .Replace("-", string.Empty);
        
        int[] multi1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multi2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string hasCpf;
        string digit;
        int sum;
        int rest;
        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;
        hasCpf = cpf.Substring(0, 9);
        sum = 0;

        for(int i=0; i<9; i++)
            sum += int.Parse(hasCpf[i].ToString()) * multi1[i];
        rest = sum % 11;
        if ( rest < 2 )
            rest = 0;
        else
            rest = 11 - rest;
        digit = rest.ToString();
        hasCpf = hasCpf + digit;
        sum = 0;
        for(int i=0; i<10; i++)
            sum += int.Parse(hasCpf[i].ToString()) * multi2[i];
        rest = sum % 11;
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;
        digit = digit + rest.ToString();
        return cpf.EndsWith(digit);
    }
    
    public static string NormalizeEmail(this string email)
    {
        if (string.IsNullOrEmpty(email))
            return string.Empty;

        var normalizedEmail = email
            .Trim()
            .ToLowerInvariant();

        return normalizedEmail;
    }
    
    public static bool IsValidEmail(this string email)
    {
        return !string.IsNullOrWhiteSpace(email) && _emailRegex.IsMatch(email);
    }
    
    public static string Truncate(this string value, int maxLength = 20)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        value = value.Trim();
        
        return value.Length <= maxLength ? value : value[..maxLength];
    }
    
    public static string RemoveBase64Prefix(string base64Input)
    {
        if (string.IsNullOrWhiteSpace(base64Input))
        {
            throw new ArgumentException("The provided Base64 string cannot be null or whitespace.", nameof(base64Input));
        }
        
        var commaIndex = base64Input.IndexOf(',');
        return commaIndex >= 0 ?
            base64Input.Substring(commaIndex + 1) :
            base64Input;
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
        return !string.IsNullOrWhiteSpace(extension) && AllowedExtensions.Contains(extension);
    }
}