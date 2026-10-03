using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Learnify.Web.Infrastructure.RazorUtil
{
    public interface IRenderViewToString
    {
        Task<string> RenderToStringAsync(string viewName, object model, PageContext context);
    }

    public class RenderViewToString(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider) : IRenderViewToString
    {
        public async Task<string> RenderToStringAsync(string viewName, object model, PageContext context)
        {
            var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };

            await using var sw = new StringWriter();
            var viewResult = razorViewEngine.FindView(context, viewName, false);

            if (viewResult.View == null)
            {
                throw new ArgumentNullException($"{viewName} does not match any available view");
            }

            var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };

            var viewContext = new ViewContext(
                context,
                viewResult.View,
                viewDictionary,
                new TempDataDictionary(context.HttpContext, tempDataProvider),
                sw,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            return sw.ToString();
        }
    }
}
