using BlogModule.Services;
using BlogModule.Services.DTOs.Query;
using CoreModule.Domain.Courses.Enums;
using CoreModule.Facade.Courses;
using CoreModule.Query.Courses.DTOs;
using Learnify.Web.ViewModels;

namespace Learnify.Web.Infrastructure.Services;

public interface IHomePageService
{
    public Task<HomePageViewModel> GetData();
}

public class HomePageService(ICourseFacade courseFacade, IBlogService service) : IHomePageService
{
    public async Task<HomePageViewModel> GetData()
    {
        var course = await courseFacade.GetByFilter(new CourseFilterParams()
        {
            Take = 8,
            PageId = 1,
            FilterSort = CourseFilterSort.Latest,
            ActionStatus = CourseActionStatus.Active,
        });

        var posts = await service.GetPostByFilter(new BlogPostFilterParams
        {
            PageId = 1,
            Take = 6,
        });

        var model = new HomePageViewModel()
        {
            LatestCourses = course.Datas.Select(s => new CourseCardViewModel
            {
                Title = s.Title,
                Slug = s.Slug,
                ImageName = s.ImageName,
                Price = s.Price,
                Duration = s.GetDuration(),
                Visit = 0,
                CommentCount = 0,
                TeacherName = s.TeacherName
            }).ToList(),
            LatestArticles = posts.Datas
        };

        return model;
    }
}