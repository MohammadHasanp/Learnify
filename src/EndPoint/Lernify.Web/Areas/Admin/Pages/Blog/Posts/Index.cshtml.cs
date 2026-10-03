using BlogModule.Services;
using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;
using Learnify.Web.Infrastructure.RazorUtil;
using Learnify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Common.Application;
using Learnify.Web.ViewModels;
using UserModule.Core.Services;

namespace Learnify.Web.Areas.Admin.Pages.Blog.Posts
{
    public class IndexModel(IBlogService service, IUserService userService, IRenderViewToString renderViewToString) : BaseRazorFilter<BlogPostFilterParams>
    {
        public BlogPostFilterResult FilterResult { get; set; } = null!;
        public List<BlogCategoryDto?>? Categories { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [BindProperty]
        public string Title { get; set; } = null!;

        [Display(Name = "نام نویسنده")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [BindProperty]
        public string OwnerName { get; set; } = null!;

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [BindProperty]
        public string Description { get; set; } = null!;

        [Display(Name = "slug")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [BindProperty]
        public string Slug { get; set; } = null!;

        [Display(Name = "عکس مقاله")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [BindProperty]
        public IFormFile ImageFile { get; set; } = null!;


        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [BindProperty]
        public Guid CategoryId { get; set; }
        public async Task OnGet()
        {
            FilterResult = await service.GetPostByFilter(FilterParams);
            Categories = service.GetAllCategories();

            var user = await userService.GetUserById(User.GetUserId());
            if (user != null)
                OwnerName = user.Name + " " + user.Family;
        }

        public async Task<IActionResult> OnGetShowEditPage(Guid id)
        {
            return await AjaxTryCatch(async () =>
            {
                var post = await service.GetPostById(id);
                if (post == null)
                    return OperationResult<string>.NotFound();

                var categories = service.GetAllCategories();
                var viewModel = new EditPostViewModel
                {
                    Categories = categories,
                    Id = post.Id,
                    CategoryId = post.CategoryId,
                    Title = post.Title,
                    UserId = post.UserId,
                    OwnerName = post.WriterName,
                    Description = post.Description,
                    Slug = post.Slug,
                };
                var view = await renderViewToString.RenderToStringAsync("_Edit", viewModel, PageContext);
                return OperationResult<string>.Success(view);
            });
        }
        public async Task<IActionResult> OnPostEdit(EditPostViewModel viewModel)
        {
            return await AjaxTryCatch(async () => await service.EditPost(new EditPostCommand()
            {
                Title = viewModel.Title,
                Slug = viewModel.Slug,
                CategoryId = viewModel.CategoryId,
                Description = viewModel.Description,
                Id = viewModel.Id,
                ImageFile = viewModel.ImageFile,
                WriterName = viewModel.OwnerName,
            }));
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            return await AjaxTryCatch(() => service.DeletePost(id));
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await service.CreatePost(new CreatePostCommand()
            {
                UserId = User.GetUserId(),
                WriterName = OwnerName,
                CategoryId = CategoryId,
                Description = Description,
                Slug = Slug,
                Title = Title,
                ImageFile = ImageFile
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}

