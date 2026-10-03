using Common.Application;
using Microsoft.EntityFrameworkCore;
using User.Module.Data.Context;

namespace UserModule.Core.Commands.Users.UserNotifications.Delete;

public class DeleteUserNotificationHandler(UserContext userContext) : IBaseCommandHandler<DeleteUserNotificationCommand>
{
    private readonly UserContext _userContext = userContext;
    public async Task<OperationResult> Handle(DeleteUserNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _userContext.Notifications.FirstOrDefaultAsync(n => n.UserId == request.UserId &&
        n.Id == request.NotificationId, cancellationToken: cancellationToken);
        if (notification == null)
            return OperationResult.NotFound();

        _userContext.Notifications.Remove(notification);
        await _userContext.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}