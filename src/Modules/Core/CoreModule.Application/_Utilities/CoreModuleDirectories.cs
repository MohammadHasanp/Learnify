namespace CoreModule.Application._Utilities;

public class CoreModuleDirectories
{
    public static string CvFileNames = "wwwroot/core/teacher";
    public static string CourseImages = "wwwroot/core/course/Images";
    public static string ImageUpload = "wwwroot/core/Images/Upload";
    public static string CourseVideos(Guid courseId) => $"wwwroot/core/course/videos/{courseId}";
    public static string CourseDemo(Guid courseId) => $"wwwroot/core/course/{courseId}";

    public static string CourseEpisode(Guid courseId, Guid episodeToken) => $"wwwroot/core/course/{courseId}/episodes/{episodeToken}";

    public static string GetCourseEpisode(Guid courseId, Guid episodeToken, string fileName)
        => $"{CourseEpisode(courseId, episodeToken).Replace("wwwroot", "")}/{fileName}";

    public static string GetCourseVideos(Guid courseId, string fileName) => $"{CourseVideos(courseId).Replace("wwwroot", "")}/{fileName}";
    public static string GetCourseImage(string imageName) => $"{CourseImages.Replace("wwwroot", "")}/{imageName}";
    public static string GetImageUpload(string imageName) => $"{ImageUpload.Replace("wwwroot", "")}/{imageName}";
    public static string GetCvFile(string cvFileName) => $"{CvFileNames.Replace("wwwroot", "")}/{cvFileName}";
}