using Common.Application;
using Microsoft.EntityFrameworkCore;
using User.Module.Data.Context;

namespace UserModule.Core.Commands.Users.UserNotifications.DeleteAll;

public class DeleteAllUserNotificationHandler(UserContext context) : IBaseCommandHandler<DeleteAllUserNotificationCommand>
{
    private readonly UserContext _userContext = context;
    public async Task<OperationResult> Handle(DeleteAllUserNotificationCommand request, CancellationToken cancellationToken)
    {
        var notifications = await _userContext.Notifications.Where(n => n.UserId == request.UserId).ToListAsync(cancellationToken);
        if (!notifications.Any())
            return OperationResult.NotFound();

        _userContext.Notifications.RemoveRange(notifications);
        await _userContext.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}