namespace Common.Application.SecurityUtil;

using System.Security.Cryptography;
using System.Text;

public class Sha256Hasher
{
    public static string Hash(string inputValue)
    {
        if (string.IsNullOrWhiteSpace(inputValue))
            return "";

        var originalBytes = Encoding.Default.GetBytes(inputValue);
        var encodedBytes = SHA256.HashData(originalBytes);
        return Convert.ToBase64String(encodedBytes);
    }
    public static bool IsCompare(string hashText, string rawText)
    {
        var hash = Hash(rawText);
        return hashText == hash;
    }
}
