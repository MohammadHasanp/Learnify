using CoreModule.Facade.Courses;
using CoreModule.Facade.Teachers;
using CoreModule.Query.Courses.DTOs;
using Learnify.Web.Infrastructure;
using Learnify.Web.Infrastructure.RazorUtil;
using Learnify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Web.Pages.Profile.Teacher.Courses.Sections;

[ServiceFilter(typeof(TeacherActionFilter))]
public class IndexModel(ICourseFacade courseFacade, ITeacherFaced teacherFaced) : BaseRazorPage
{
    public CourseDto? CourseDto { get; set; }

    public async Task<IActionResult> OnGet(Guid courseId)
    {
        var teacher = await teacherFaced.GetByUserId(User.GetUserId());
        var course = await courseFacade.GetById(courseId);

        if (course == null || course.TeacherId != teacher?.Id)
        {
            return RedirectToPage("../Index");
        }

        CourseDto = course;
        return Page();
    }
}
