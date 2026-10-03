using BlogModule.Services;
using BlogModule.Services.DTOs.Query;
using Common.Application;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using BlogModule.Services.DTOs.Command;
using Learnify.Web.Infrastructure.RazorUtil;

namespace Learnify.Web.Areas.Admin.Pages.Blog.Categories;

public class IndexModel(IBlogService service, IRenderViewToString renderView) : BaseRazorPage
{
    private readonly IBlogService _service = service;
    private readonly IRenderViewToString _renderView = renderView;

    public List<BlogCategoryDto> Categories { get; set; } = [];


    [BindProperty]
    [Display(Name = "عنوان")]
    public string? Title { get; set; }

    [BindProperty]
    [Display(Name = "slug")]
    public string? Slug { get; set; }


    public async Task OnGet()
    {
        Categories = _service.GetAllCategories();
    }

    public async Task<IActionResult> OnGetShowEditPage(Guid id)
    {
        return await AjaxTryCatch(async () =>
        {
            var category = await _service.GetCategoryById(id);
            if (category == null)
            {
                return OperationResult<string>.NotFound();
            }

            var viewResult = await _renderView.RenderToStringAsync("_Edit", new EditCategoryCommand
            {
                Title = category.Title,
                Slug = category.Slug,
                CategoryId = id
            }, PageContext);

            return OperationResult<string>.Success(viewResult);
        });
    }
    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        return await AjaxTryCatch(() => _service.DeleteCategory(id));
    }
    public async Task<IActionResult> OnPostEdit(EditCategoryCommand command)
    {
        return await AjaxTryCatch(() => _service.EditCategory(command));
    }
    public async Task<IActionResult> OnPost()
    {
        return await AjaxTryCatch(() => _service.CreateCategory(new CreateCategoryCommand
        {
            Slug = Slug,
            Title = Title
        }));
    }
}