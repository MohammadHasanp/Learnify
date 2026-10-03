using CoreModule.Facade.CourseCategories;
using CoreModule.Query.CourseCategories.DTOs;
using Learnify.Web.Infrastructure.RazorUtil;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Web.Areas.Admin.Pages.Courses.Categories;

public class IndexModel(ICourseCategoryFacade categoryFacade) : BaseRazorPage
{
    private readonly ICourseCategoryFacade _categoryFacade = categoryFacade;

    public List<CourseCategoryDto?> CategoryDto { get; set; } = [];

    public async Task OnGet()
    {
        CategoryDto = await _categoryFacade.GetMainCategory();
    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        return await AjaxTryCatch(() => _categoryFacade.Delete(id));
    }
}

