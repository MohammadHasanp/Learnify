using System.ComponentModel.DataAnnotations;
using Common.Domain.ValueObjects;
using CoreModule.Application.Courses.Create;
using CoreModule.Domain.Courses.Enums;
using CoreModule.Facade.Courses;
using CoreModule.Facade.Teachers;
using Learnify.Web.Infrastructure;
using Learnify.Web.Infrastructure.CustomValidation.IFormFile;
using Learnify.Web.Infrastructure.RazorUtil;
using Learnify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Web.Pages.Profile.Teacher.Courses;

[ServiceFilter(typeof(TeacherActionFilter))]
[BindProperties]
public class AddModel(ITeacherFaced teacherFaced, ICourseFacade courseFacade) : BaseRazorPage
{
    [Display(Name = "دسته بندی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public Guid CategoryId { get; set; }


    [Display(Name = " زیردسته یندی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public Guid SubCategoryId { get; set; }


    [Display(Name = " عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; } = null!;


    [Display(Name = " slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; } = null!;


    [Display(Name = " توضیحات")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Description { get; set; } = null!;


    [Display(Name = " عکس دوره ")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileImage(ErrorMessage = "{0} نامعتبر است")]
    public IFormFile ImageFile { get; set; } = null!;


    [Display(Name = " ویدیو معرفی")]
    public IFormFile? VideoFile { get; set; }


    [Display(Name = "قیمت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Price { get; set; }

    [Display(Name = "سطح دوره")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public CourseLevel CourseLevel { get; set; }



    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var teacher = await teacherFaced.GetByUserId(User.GetUserId());

        var result = await courseFacade.Create(new CreateCourseCommand(
            teacher!.Id, CategoryId, SubCategoryId, Title, Slug, Description, ImageFile, VideoFile, Price,
            new SeoData(Title, Title, null, true, "", "")
            , CourseLevel, CourseActionStatus.Pending));

        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

