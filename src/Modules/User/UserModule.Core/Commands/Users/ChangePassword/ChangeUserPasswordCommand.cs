using Common.Application;

namespace UserModule.Core.Commands.Users.ChangePassword;

public record ChangeUserPasswordCommand(Guid UserId, string CurrentPasssword, string NewPassword) : IBaseCommand;
