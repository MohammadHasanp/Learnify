using Common.Application;
using Microsoft.EntityFrameworkCore;
using User.Module.Data.Context;

namespace UserModule.Core.Commands.Users.UserNotifications.Seen;

public class SeenNotificationHandler(UserContext context) : IBaseCommandHandler<SeenNotificationCommand>
{
    private readonly UserContext _userContext = context;
    public async Task<OperationResult> Handle(SeenNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _userContext.Notifications.FirstOrDefaultAsync(n => n.UserId == request.UserId && n.Id == request.NotificatiuonId, cancellationToken: cancellationToken);
        if (notification == null)
            return OperationResult.NotFound();

        notification.IsSeen = true;
        _userContext.Update(notification);
        await _userContext.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}