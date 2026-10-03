using System.ComponentModel.DataAnnotations;
using Common.Domain.Utilities;
using CoreModule.Application.CourseCategories.AddChild;
using CoreModule.Application.CourseCategories.Create;
using CoreModule.Facade.CourseCategories;
using Learnify.Web.Infrastructure.RazorUtil;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Web.Areas.Admin.Pages.Courses.Categories;

[BindProperties]
public class AddModel(ICourseCategoryFacade categoryFacade) : BaseRazorPage
{
    private readonly ICourseCategoryFacade _categoryFacade = categoryFacade;

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; } = null!;

    [Display(Name = "slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; } = null!;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost([FromQuery] Guid? parentId)
    {
        if (parentId != null)
        {
            var result = await _categoryFacade.AddChild(new AddChildCourseCategoryCommand(Title, Slug, (Guid)parentId));
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }

        else
        {
            var result = await _categoryFacade.Create(new CreateCourseCategoryCommand(Title, Slug.ToSlug()));
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}

