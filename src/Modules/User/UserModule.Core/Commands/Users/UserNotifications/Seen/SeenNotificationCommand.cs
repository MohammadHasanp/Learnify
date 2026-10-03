using Common.Application;

namespace UserModule.Core.Commands.Users.UserNotifications.Seen;

public record SeenNotificationCommand(Guid UserId, Guid NotificatiuonId) : IBaseCommand;
