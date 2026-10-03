namespace Common.Application.FileUtil.Validations;

using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Globalization;
using System.IO;


public static class FileValidation
{
    public static bool IsValidFile(this IFormFile file)
    {
        var path = Path.GetExtension(file.FileName);
        path = path.ToLower(CultureInfo.CurrentCulture);
        return path is ".mp4" or ".mp3" or ".zip" or
            ".rar" or ".wav" or ".docx" or
            ".mmf" or ".m4a" or ".ogg" or
            ".doc" or ".pdf" or ".txt" or
            ".xls" or ".xla" or ".xlsx" or
            ".ppt" or ".pptx" or ".gif" or
            ".jpg" or ".png" or ".tif" or ".wmv" or
            ".bmp" or ".wmf" or ".gif" or ".log";
    }

    public static bool IsValidCompressFile(this IFormFile file)
    {
        var path = Path.GetExtension(file.FileName);
        path = path.ToLower(CultureInfo.CurrentCulture);
        return path is ".zip" or ".rar";
    }

    public static bool IsValidVideoFile(this IFormFile file)
    {
        var path = Path.GetExtension(file.FileName);
        path = path.ToLower(CultureInfo.CurrentCulture);

        return path is ".mp4";
    }

    public static bool IsValidImageFile(this IFormFile ImageFile)
    {
        var path = Path.GetExtension(ImageFile.FileName);
        path = path.ToLower(CultureInfo.CurrentCulture);
        return path is ".jpg" or ".png" or ".bmp" or ".svg" or ".jpeg";
    }

    public static bool IsImage(this IFormFile? file)
    {
        if (file == null)
            return false;
        try
        {
            var img = Image.FromStream(file.OpenReadStream());
            return true;
        }
        catch
        {
            return false;
        }
    }
}
