using Learnify.Web.Infrastructure.RazorUtil;
using Learnify.Web.Infrastructure.Services;
using Learnify.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Learnify.Web.Pages;

[Authorize]
public class IndexModel(IHomePageService pageService) : BaseRazorPage
{
    public HomePageViewModel HomePageData { get; set; } = new();

    public async Task OnGet()
    {
        HomePageData = await pageService.GetData();
    }
}
