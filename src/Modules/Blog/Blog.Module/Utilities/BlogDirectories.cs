namespace BlogModule.Utilities;

public static class BlogDirectories
{
    public static readonly string PostImage = "wwwroot/core/Blog/Images";

    public static string GetPostImage(string? imageName)
    {
        return $"{PostImage.Replace("wwwroot", "")}/{imageName}";
    }
}
