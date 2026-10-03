namespace UserModule.Core;

public class UserModuleDirectories
{
    public static readonly string UserAvatar = "wwwroot/users/images";

    public static string GetUserAvatar(string? imageName) => $"{UserAvatar.Replace("wwwroot", "")}/{imageName}";
}