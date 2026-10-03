using CoreModule.Domain.Teachers.Enums;
using CoreModule.Facade.Teachers;
using Learnify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Learnify.Web.Infrastructure;

public class TeacherActionFilter(ITeacherFaced teacherFaced) : ActionFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.HttpContext.User.Identity is { IsAuthenticated: false })
            context.Result = new RedirectResult("/");

        var teacher = await teacherFaced.GetByUserId(context.HttpContext.User.GetUserId());
        if (teacher == null || teacher.TeacherStatus != TeacherStatus.Active)
            context.Result = new RedirectResult("/Profile");

        await next();
    }
}