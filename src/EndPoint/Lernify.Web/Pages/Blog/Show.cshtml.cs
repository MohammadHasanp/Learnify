using BlogModule.Services;
using BlogModule.Services.DTOs.Query;
using CommentModule.Domain;
using CommentModule.Services;
using CommentModule.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Learnify.Web.Pages.Blog
{
    public class ShowModel(IBlogService blogService, ICommentService service) : PageModel
    {
        public BlogPostDto BlogPost { get; set; } = null!;
        public CommentFilterResult? CommentFilterResult { get; set; }

        public async Task<IActionResult> OnGet(string slug)
        {
            var article = await blogService.GetPostBySlug(slug);
            if (article == null)
            {
                return NotFound();
            }

            CommentFilterResult = await service.GetCommentByFilter(new CommentFilterParams()
            {
                EntityId = article.Id,
                CommentType = CommentType.Article,
            });

            BlogPost = article;
            await blogService.AddPostVisit(article.Id);
            return Page();
        }
    }
}
