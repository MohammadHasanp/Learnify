using Common.Application;

namespace UserModule.Core.Commands.Users.UserNotifications.DeleteAll;

public record DeleteAllUserNotificationCommand(Guid UserId) : IBaseCommand;
