using Common.Application;

namespace UserModule.Core.Commands.Users.Edit;

public record EditUserCommand(Guid UserId, string Family, string Name, string? Email) : IBaseCommand;
