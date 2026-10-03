using Common.Domain.Utilities;
using Learnify.Web.Infrastructure.RazorUtil;
using Learnify.Web.Infrastructure.Util;
using UserModule.Core.Queries.Users.DTOs;
using UserModule.Core.Services;

namespace Learnify.Web.Areas.Admin.Pages.Users
{
    public class IndexModel(IUserService service) : BaseRazorFilter<UserFilterParams>
    {
        public UserFilterResult? FilterResult { get; set; }

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
            FilterResult = await service.GetUserByFilter(FilterParams);
        }
    }
}
