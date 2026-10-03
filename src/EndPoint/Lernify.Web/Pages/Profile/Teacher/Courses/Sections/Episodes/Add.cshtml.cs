using CoreModule.Application.Courses.Episodes.Add;
using CoreModule.Facade.Courses;
using CoreModule.Facade.Teachers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Learnify.Web.Infrastructure.Util;
using Learnify.Web.Infrastructure;
using Learnify.Web.Infrastructure.CustomValidation.IFormFile;
using Learnify.Web.Infrastructure.RazorUtil;

namespace Learnify.Web.Pages.Profile.Teacher.Courses.Sections.Episodes;

[ServiceFilter(typeof(TeacherActionFilter))]
[BindProperties]
public class AddModel(ICourseFacade courseFacade, ITeacherFaced teacherFaced) : BaseRazorPage
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; } = null!;

    [Display(Name = "عنوان انگلیسی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string EnglishTitle { get; set; } = null!;

    [Display(Name = "زمان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [RegularExpression(@"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$", ErrorMessage =
        "لطفا زمان را با فرمت درست وارد کنید")]
    public TimeSpan Time { get; set; }

    [Display(Name = "فایل ضمیمه")]
    public IFormFile? AttachmentFile { get; set; }

    [Display(Name = "ویدیو")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileType("mp4", ErrorMessage = "ویدیو نامعتبر است")]
    public IFormFile VideoFile { get; set; } = null!;

    [Display(Name = "این بخش رایگان است!")]
    public bool IsFree { get; set; }

    public async Task<IActionResult> OnGet(Guid courseId)
    {
        var teacher = await teacherFaced.GetByUserId(User.GetUserId());
        var course = await courseFacade.GetById(courseId);

        if (course == null || course.TeacherId != teacher?.Id)
        {
            return RedirectToPage("/");
        }

        return Page();
    }

    public async Task<IActionResult> OnPost(Guid courseId, Guid sectionId)
    {
        var result = await courseFacade.AddEpisode(new AddEpisodeCommand(courseId, sectionId, Title, EnglishTitle,
            Time, AttachmentFile, VideoFile, false, IsFree));

        return RedirectAndShowAlert(result, RedirectToPage("../Index", new { courseId }));
    }
}

