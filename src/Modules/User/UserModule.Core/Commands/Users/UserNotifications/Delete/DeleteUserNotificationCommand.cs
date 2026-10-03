using Common.Application;

namespace UserModule.Core.Commands.Users.UserNotifications.Delete;

public record DeleteUserNotificationCommand(Guid UserId, Guid NotificationId) : IBaseCommand;
