using CoreModule.Application.Courses.Episodes.Accept;
using CoreModule.Application.Courses.Episodes.Delete;
using CoreModule.Facade.Courses;
using CoreModule.Query.Courses.DTOs;
using Learnify.Web.Infrastructure.RazorUtil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Learnify.Web.Areas.Admin.Pages.Courses.Sections;

public class IndexModel(ICourseFacade courseFacade) : BaseRazorPage
{
    private readonly ICourseFacade _courseFacade = courseFacade;


    public CourseDto? Course { get; set; }

    public async Task<IActionResult> OnGet(Guid courseId)
    {
        var course = await _courseFacade.GetById(courseId);
        if (course == null)
            return RedirectToPage("../Index");

        Course = course;
        return Page();
    }

    public async Task<IActionResult> OnPostDelete(Guid courseId, Guid episodeId)
    {
        return await AjaxTryCatch(() => _courseFacade.DeleteEpisode(new DeleteCourseEpisodeCommand(courseId, episodeId)));
    }

    public async Task<IActionResult> OnPostAccept(Guid courseId, Guid episodeId)
    {
        return await AjaxTryCatch(() => _courseFacade.AcceptEpisode(new AcceptCourseEpisodeCommand(courseId, episodeId)));
    }
}
