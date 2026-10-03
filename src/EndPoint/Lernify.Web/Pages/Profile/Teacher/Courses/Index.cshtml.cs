using CoreModule.Facade.Courses;
using CoreModule.Facade.Teachers;
using CoreModule.Query.Courses.DTOs;
using Learnify.Web.Infrastructure;
using Learnify.Web.Infrastructure.RazorUtil;
using Learnify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Web.Pages.Profile.Teacher.Courses;

[ServiceFilter(typeof(TeacherActionFilter))]
public class IndexModel(ICourseFacade courseFacade, ITeacherFaced teacherFaced) : BaseRazorFilter<CourseFilterParams>
{
    public CourseFilterResult FilterResult { get; set; } = null!;

    public async Task OnGet()
    {
        var teacher = await teacherFaced.GetByUserId(User.GetUserId());
        FilterParams.TeacherId = teacher?.Id;
        FilterResult = await courseFacade.GetByFilter(FilterParams);
    }
}