namespace Common.Application.SecurityUtil;

using System.Security.Cryptography;
using System.Text;
public static class PasswordHelper
{
    public static string EncodePasswordMd5(string pass) //Encrypt using MD5   
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(pass);
        var hashBytes = md5.ComputeHash(inputBytes);

        // Convert the byte array to hexadecimal string
        var sb = new StringBuilder();
        for (var i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(value: hashBytes[i].ToString("X2"));
        }
        return sb.ToString();
    }
}
