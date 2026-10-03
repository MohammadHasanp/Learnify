using CommentModule.Domain;
using CommentModule.Services;
using CommentModule.Services.DTOs;
using Common.Application;
using Learnify.Web.Infrastructure;
using Learnify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Web.Controllers;

public class CommentController(ICommentService service) : BaseController
{

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateComment(CreateCommentCommand command)
    {
        return await AjaxTryCatch(() => service.Create(command));
    }

    [HttpGet]
    [Route("/comment/getByFilter")]
    public async Task<IActionResult> GetComment([FromQuery] Guid entityId, CommentType commentType, int pageId)
    {
        var model = await service.GetCommentByFilter(new CommentFilterParams()
        {
            EntityId = entityId,
            CommentType = commentType,
            PageId = pageId,
        });
        return PartialView("Comments/_Comments", model);
    }

    [HttpPost("/comment/delete")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid commentId)
    {
        var comment = await service.GetCommentById(commentId);
        if (comment == null || comment.UserId != User.GetUserId())
        {
            return await AjaxTryCatch(() => Task.FromResult(OperationResult.NotFound()));
        }
        return await AjaxTryCatch(() => service.Delete(commentId));
    }
}