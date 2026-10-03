using Learnify.Web.Infrastructure.RazorUtil;
using TicketModule.Services;
using TicketModule.Services.DTOs.Query;

namespace Learnify.Web.Areas.Admin.Pages.Tickets;


public class IndexModel(ITicketService service) : BaseRazorFilter<TicketFilterParams>
{
    private readonly ITicketService _service = service;

    public TicketFilterResult FilterResult { get; set; } = null!;
    public async Task OnGet()
    {
        FilterResult = await _service.GetTicketByFilter(FilterParams);
    }
}

