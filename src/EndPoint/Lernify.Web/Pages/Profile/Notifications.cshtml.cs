using Learnify.Web.Infrastructure.RazorUtil;
using Learnify.Web.Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;
using UserModule.Core.Commands.Users.UserNotifications.Delete;
using UserModule.Core.Commands.Users.UserNotifications.DeleteAll;
using UserModule.Core.Commands.Users.UserNotifications.Seen;
using UserModule.Core.Queries.Users.DTOs;
using UserModule.Core.Services;

namespace Learnify.Web.Pages.Profile;

public class NotificationsModel(IUserNotificationService service) : BaseRazorFilter<UserNotificationFilterParams>
{
    public required UserNotificationFilterResult FilterResult { get; set; }

    public async Task OnGet()
    {
        FilterResult = await service.GetByFilter(new UserNotificationFilterParams()
        {
            IsSeen = null,
            PageId = FilterParams.PageId,
            Take = 1,
            UserId = User.GetUserId(),
        });
    }

    public async Task<IActionResult> OnPostSeenNotification(Guid id)
    {
        var result = await service.Seen(new SeenNotificationCommand(User.GetUserId(), id));
        return RedirectAndShowAlert(result, RedirectToPage("Notifications"));
    }

    public async Task<IActionResult> OnPostDeleteAll()
    {
        return await AjaxTryCatch(() => service.DeleteAll(new DeleteAllUserNotificationCommand(User.GetUserId())));
    }

    public async Task<IActionResult> OnPostDeleteNotification(Guid id)
    {
        return await AjaxTryCatch(() => service.Delete(new DeleteUserNotificationCommand(User.GetUserId(), id)));
    }
}
