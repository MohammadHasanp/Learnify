using CoreModule.Facade.CourseCategories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CoreModule.Application.CourseCategories.Edit;
using Learnify.Web.Infrastructure.RazorUtil;

namespace Learnify.Web.Areas.Admin.Pages.Courses.Categories
{
    [BindProperties]
    public class EditModel(ICourseCategoryFacade categoryFacade) : BaseRazorPage
    {

        private readonly ICourseCategoryFacade _categoryFacade = categoryFacade;

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; } = null!;

        [Display(Name = "slug")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Slug { get; set; } = null!;



        public async Task<IActionResult> OnGet(Guid id)
        {
            var category = await _categoryFacade.GetById(id);

            if (category is null)
                return RedirectToPage("Index");

            Slug = category.Slug;
            Title = category.Title;
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            var result = await _categoryFacade.Edit(new EditCourseCategoryCommand(id, Title, Slug));
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}
