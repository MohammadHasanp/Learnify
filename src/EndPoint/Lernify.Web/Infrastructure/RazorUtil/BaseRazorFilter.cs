using Common.Query;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Web.Infrastructure.RazorUtil;

public class BaseRazorFilter<TFilterParam> : BaseRazorPage where TFilterParam : BaseFilterParam
{
    [BindProperty(SupportsGet = true)]
    public required TFilterParam FilterParams { get; set; }
}
