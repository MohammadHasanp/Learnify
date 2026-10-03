using CoreModule.Application.Courses.Episodes.Edit;
using CoreModule.Facade.Courses;
using CoreModule.Query.Courses.Episodes.DTOs;
using Learnify.Web.Infrastructure.RazorUtil;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Learnify.Web.Infrastructure.CustomValidation.IFormFile;

namespace Learnify.Web.Areas.Admin.Pages.Courses.Sections;

public class EditEpisodeModel(ICourseFacade courseFacade) : BaseRazorPage
{
    [BindProperty]
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; } = null!;

    [BindProperty]
    [Display(Name = "زمان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [RegularExpression(@"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$", ErrorMessage =
        "لطفا زمان را با فرمت درست وارد کنید")]
    public TimeSpan Time { get; set; }

    [BindProperty]
    [Display(Name = "ویدیو")]
    [FileType("mp4", ErrorMessage = "فایل ویدیو نامعتبر است")]
    public IFormFile? VideoFile { get; set; }

    [BindProperty]
    [Display(Name = "فایل ضمیمه")]
    [FileType("rar", ErrorMessage = "فایل ضمینه نامعتبر است")]
    public IFormFile? AttachmentFile { get; set; }

    [BindProperty]
    [Display(Name = "وضعیت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public bool IsActive { get; set; }

    [BindProperty]
    [Display(Name = "این بخش رایگان است!")]
    public bool IsFree { get; set; }

    public Guid? CourseId { get; set; }
    public EpisodeDto? EpisodeDto { get; set; } = new();

    public async Task<IActionResult> OnGet(Guid episodeId, Guid courseId)
    {
        var episode = await courseFacade.GetEpisodeById(episodeId);
        if (episode == null)
            return RedirectToPage("../Index");

        Title = episode.Title;
        IsActive = episode.IsActive;
        Time = episode.Time;
        IsFree = episode.IsFree;
        EpisodeDto = episode;
        CourseId = courseId;
        return Page();
    }

    public async Task<IActionResult> OnPost(Guid episodeId, Guid courseId)
    {
        var episode = await courseFacade.GetEpisodeById(episodeId);
        if (episode == null)
            return RedirectToPage("../Index");

        var result = await courseFacade.
            EditEpisode(new EditEpisodeCommand(episodeId, courseId, episode.SectionId, Title, Time, VideoFile, AttachmentFile, IsActive, IsFree));
        return RedirectAndShowAlert(result, RedirectToPage("Index", new { courseId }));
    }
}
