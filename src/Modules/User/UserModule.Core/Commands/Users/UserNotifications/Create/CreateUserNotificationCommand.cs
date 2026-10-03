using Common.Application;

namespace UserModule.Core.Commands.Users.UserNotifications.Create;

public record CreateUserNotificationCommand(Guid UserId, string Text, string Title) : IBaseCommand;
