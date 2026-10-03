using CommentModule.Services;
using CommentModule.Services.DTOs;
using Common.Domain.Utilities;
using Learnify.Web.Infrastructure.RazorUtil;
using Learnify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Web.Areas.Admin.Pages.Comments
{
    public class IndexModel(ICommentService service) : BaseRazorFilter<CommentFilterParams>
    {
        public AllCommentFilterResult? AllCommentFilterResult { get; set; }

        public async Task OnGet(string stDate, string enDate)
        {
            if (!string.IsNullOrWhiteSpace(stDate))
            {
                FilterParams.StartDate = stDate.ToMiladi();
            }
            if (!string.IsNullOrWhiteSpace(enDate))
            {
                FilterParams.EndDate = enDate.ToMiladi();
            }
            AllCommentFilterResult = await service.GetAllCommentByFilter(FilterParams);
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            return await AjaxTryCatch(() => service.Delete(id));
        }
    }
}
