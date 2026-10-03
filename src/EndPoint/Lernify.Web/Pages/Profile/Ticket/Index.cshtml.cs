using Microsoft.AspNetCore.Mvc;
using Learnify.Web.Infrastructure.Util;
using TicketModule.Services;
using TicketModule.Services.DTOs.Query;
using UserModule.Core.Services;
using Learnify.Web.Infrastructure.RazorUtil;

namespace Learnify.Web.Pages.Profile.Ticket;

[BindProperties]
public class IndexModel(IUserService userService, ITicketService ticketService) : BaseRazorFilter<TicketFilterParams>
{
    public required TicketFilterResult FilterResult { get; set; }
    public async Task OnGet()
    {
        FilterResult = await ticketService.GetTicketByFilter(new TicketFilterParams()
        {
            PageId = FilterParams.PageId,
            Take = 5,
            UserId = User.GetUserId()
        });
    }
}
