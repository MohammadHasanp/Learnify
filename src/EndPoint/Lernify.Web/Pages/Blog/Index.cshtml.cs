using BlogModule.Services;
using BlogModule.Services.DTOs.Query;
using Learnify.Web.Infrastructure.RazorUtil;

namespace Learnify.Web.Pages.Blog
{
    public class IndexModel(IBlogService blogService) : BaseRazorFilter<BlogPostFilterParams>
    {
        public List<BlogCategoryDto?> Categories { get; set; } = [];
        public BlogPostFilterResult FilterResult { get; set; } = null!;

        public async Task OnGet()
        {
            FilterResult = await blogService.GetPostByFilter(FilterParams);
            Categories = blogService.GetAllCategories();
        }
    }
}
