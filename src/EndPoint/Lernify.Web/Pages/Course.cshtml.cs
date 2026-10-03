using CoreModule.Application._Utilities;
using CoreModule.Facade.Courses;
using CoreModule.Query.Courses.DTOs;
using Learnify.Web.Infrastructure.RazorUtil;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Web.Pages
{
    public class CourseModel(ICourseFacade courseFacade) : BaseRazorPage
    {
        private readonly ICourseFacade _courseFacade = courseFacade;

        public CourseDto? Course { get; set; }

        public async Task<IActionResult> OnGet(string slug)
        {
            var course = await courseFacade.GetBySlug(slug);
            if (course == null!)
                return NotFound();

            Course = course;
            return Page();
        }

        public async Task<IActionResult> OnGetShowOnline(string slug, Guid sectionId, Guid token)
        {
            var course = await _courseFacade.GetBySlug(slug);
            if (course == null)
                return NotFound();

            var section = course.Sections.First(f => f.Id == sectionId);
            var episode = section.Episodes.FirstOrDefault(r => r.Token == token);
            if (episode == null)
                return NotFound();

            var src = CoreModuleDirectories.GetCourseEpisode(course.Id, token, episode.VideoName);
            return Content(src);
        }

        public async Task<IActionResult> OnGetDownload(string slug, Guid sectionId, Guid token)
        {
            var course = await _courseFacade.GetBySlug(slug);
            if (course == null)
                return NotFound();

            var section = course.Sections.First(f => f.Id == sectionId);
            var episode = section.Episodes.FirstOrDefault(r => r.Token == token);
            if (episode == null)
                return NotFound();

            return File(CoreModuleDirectories.GetCourseEpisode(course.Id, token, episode.VideoName),
                "application/force-download", episode.VideoName);
        }
    }
}
