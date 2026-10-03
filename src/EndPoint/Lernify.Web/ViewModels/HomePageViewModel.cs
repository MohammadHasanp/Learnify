using BlogModule.Services.DTOs.Query;

namespace Learnify.Web.ViewModels;

public class HomePageViewModel
{
    public List<CourseCardViewModel> LatestCourses { get; set; } = [];
    public List<BlogPostFilterData> LatestArticles { get; set; } = [];
}

public class CourseCardViewModel
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string ImageName { get; set; } = null!;
    public int Price { get; set; }
    public string Duration { get; set; } = null!;
    public int Visit { get; set; }
    public int CommentCount { get; set; }
    public string TeacherName { get; set; } = null!;
}