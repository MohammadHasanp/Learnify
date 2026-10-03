using CoreModule.Domain.Courses.Enums;
using CoreModule.Facade.Courses;
using CoreModule.Query.Courses.DTOs;
using Learnify.Web.Infrastructure.RazorUtil;

namespace Learnify.Web.Pages;

public class CoursesModel(ICourseFacade courseFacade) : BaseRazorFilter<CourseFilterParams>
{
    private readonly ICourseFacade _courseFacade = courseFacade;

    public CourseFilterResult FilterResult { get; set; } = null!;

    public async Task OnGet()
    {
        FilterParams.ActionStatus = CourseActionStatus.Active;
        FilterParams.TeacherId = null;
        FilterResult = await _courseFacade.GetByFilter(FilterParams);
    }
}
