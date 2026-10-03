using Common.Application;

namespace UserModule.Core.Commands.Roles.Delete;

public record DeleteRoleCommand(Guid RoleId) : IBaseCommand;