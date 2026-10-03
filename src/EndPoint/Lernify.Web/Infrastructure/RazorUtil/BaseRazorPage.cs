using Common.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Learnify.Web.Infrastructure.RazorUtil
{
    [ValidateAntiForgeryToken]
    public class BaseRazorPage : PageModel
    {
        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (context.HandlerMethod?.MethodInfo.Name == "OnPost")
                if (!context.ModelState.IsValid)
                {
                    var modelStateErrors = JoinErrors();
                    var model = JsonConvert.SerializeObject(OperationResult.Error(modelStateErrors));
                    HttpContext.Response.Cookies.Append("SystemAlert", model);
                    context.Result = Page();
                }
            base.OnPageHandlerExecuting(context);
        }

        protected IActionResult RedirectAndShowAlert(OperationResult result, IActionResult redirectPath)
        {

            var model = JsonConvert.SerializeObject(result);
            HttpContext.Response.Cookies.Append("SystemAlert", model);
            if (result.Status != EnOperationResultStatus.Success)
                return Page();
            return redirectPath;
        }

        protected IActionResult RedirectAndShowAlert(OperationResult result, IActionResult redirectPath, IActionResult errorRedirectTo)
        {

            var model = JsonConvert.SerializeObject(result);
            HttpContext.Response.Cookies.Append("SystemAlert", model);
            if (result.Status != EnOperationResultStatus.Success)
                return errorRedirectTo;
            return redirectPath;
        }
        protected IActionResult RedirectAndShowAlert(OperationResult<Guid> result, IActionResult redirectPath)
        {
            var model = JsonConvert.SerializeObject(result);
            HttpContext.Response.Cookies.Append("SystemAlert", model);
            if (result.Status != EnOperationResultStatus.Success)
                return Page();
            return redirectPath;
        }

        protected void SuccessAlert()
        {
            var model = JsonConvert.SerializeObject(OperationResult.Success());
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }
        protected void SuccessAlert(string message)
        {
            var model = JsonConvert.SerializeObject(OperationResult.Success(message));
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }
        protected void ErrorAlert()
        {
            var model = JsonConvert.SerializeObject(OperationResult.Error());
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }
        protected void ErrorAlert(string message)
        {
            var model = JsonConvert.SerializeObject(OperationResult.Error(message));
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }

        protected string JoinErrors()
        {
            var errors = new Dictionary<string, List<string>>();

            if (!ModelState.IsValid)
            {
                if (ModelState.ErrorCount > 0)
                {
                    for (var i = 0; i < ModelState.Values.Count(); i++)
                    {
                        var key = ModelState.Keys.ElementAt(i);
                        var value = ModelState.Values.ElementAt(i);

                        if (value.ValidationState == ModelValidationState.Invalid)
                        {
                            errors.Add(key, value.Errors.Select(x => string.IsNullOrEmpty(x.ErrorMessage) ? x.Exception?.Message : x.ErrorMessage).ToList()!);
                        }
                    }
                }
            }

            var error = string.Join("<br/>", errors.Select(x => $"{string.Join(" - ", x.Value)}"));
            return error;
        }


        public async Task<ContentResult> AjaxTryCatch(Func<Task<OperationResult>> func, bool isSuccessReloadPage = true, bool isErrorReloadPage = false,
               bool checkModelState = false)
        {
            try
            {
                var isPost = PageContext.HttpContext.Request.Method == "POST";
                if (isPost && !ModelState.IsValid && checkModelState)
                {
                    var errors = JoinErrors();
                    var modelError = new AjaxResult()
                    {
                        Status = EnOperationResultStatus.Error,
                        Title = "عملیات ناموفق",
                        Message = errors,
                        IsReloadPage = isErrorReloadPage,
                    };
                    var jsonResult = JsonConvert.SerializeObject(modelError);
                    return Content(jsonResult);
                }

                var res = await func().ConfigureAwait(false);
                var model = new AjaxResult()
                {
                    Status = res.Status,
                    Title = null,
                    Message = res.Message
                };
                switch (res.Status)
                {
                    case EnOperationResultStatus.Success:
                        {
                            model.IsReloadPage = isSuccessReloadPage;
                            var jsonResult = JsonConvert.SerializeObject(model);
                            return Content(jsonResult);
                        }
                    case EnOperationResultStatus.Error:
                        {
                            model.IsReloadPage = isErrorReloadPage;
                            var jsonResult = JsonConvert.SerializeObject(model);
                            return Content(jsonResult);
                        }
                    case EnOperationResultStatus.NotFound:
                        {
                            model.IsReloadPage = isErrorReloadPage;
                            model.Title ??= "نتیجه ای یافت نشد";
                            var jsonResult = JsonConvert.SerializeObject(model);
                            return Content(jsonResult);
                        }
                    default:
                        {
                            model.IsReloadPage = isSuccessReloadPage;
                            var jsonResult = JsonConvert.SerializeObject(model);
                            return Content(jsonResult);
                        }
                }
            }
            catch (Exception ex)
            {
                var res = OperationResult.Error(ex.Message);
                var model = new AjaxResult()
                {
                    Status = res.Status,
                    Title = null,
                    Message = res.Message,
                    IsReloadPage = isErrorReloadPage
                };
                var jsonResult = JsonConvert.SerializeObject(model);
                return Content(jsonResult);
            }
        }

        public async Task<ContentResult> AjaxTryCatch<T>(Func<Task<OperationResult<T>>> func,
            bool isSuccessReloadPage = false,
            bool isErrorReloadPage = false)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = JoinErrors();
                    var modelError = new AjaxResult()
                    {
                        Status = EnOperationResultStatus.Error,
                        Title = "عملیات ناموفق",
                        Message = errors,
                        IsReloadPage = isErrorReloadPage,
                        Data = default(T)
                    };
                    var jsonResult = JsonConvert.SerializeObject(modelError);
                    return Content(jsonResult);
                }

                var res = await func().ConfigureAwait(false);
                var model = new AjaxResult()
                {
                    Status = res.Status,
                    Title = null,
                    IsReloadPage = isSuccessReloadPage,
                    Message = res.Message,
                    Data = res.Data
                };
                switch (res.Status)
                {
                    case EnOperationResultStatus.Success:
                        {
                            model.IsReloadPage = isSuccessReloadPage;
                            var jsonResult = JsonConvert.SerializeObject(model);
                            return Content(jsonResult);
                        }
                    case EnOperationResultStatus.Error:
                        {
                            model.IsReloadPage = isErrorReloadPage;

                            var jsonResult = JsonConvert.SerializeObject(model);
                            return Content(jsonResult);
                        }
                    case EnOperationResultStatus.NotFound:
                        {
                            model.IsReloadPage = isErrorReloadPage;
                            model.Title ??= "نتیجه ای یافت نشد";
                            var jsonResult = JsonConvert.SerializeObject(model);
                            return Content(jsonResult);
                        }
                    default:
                        {
                            model.IsReloadPage = isSuccessReloadPage;
                            var jsonResult = JsonConvert.SerializeObject(model);
                            return Content(jsonResult);
                        }
                }
            }
            catch (Exception ex)
            {
                var res = OperationResult.Error(ex.Message);
                var model = new AjaxResult()
                {
                    Status = res.Status,
                    Title = null,
                    Message = res.Message,
                    IsReloadPage = isErrorReloadPage
                };
                var jsonResult = JsonConvert.SerializeObject(model);
                return Content(jsonResult);
            }
        }
        public class AjaxResult
        {
            public string Message { get; set; } = null!;
            public string? Title { get; set; }
            public bool IsReloadPage { get; set; }
            public object? Data { get; set; }
            public EnOperationResultStatus Status { get; set; }
        }
    }
}
