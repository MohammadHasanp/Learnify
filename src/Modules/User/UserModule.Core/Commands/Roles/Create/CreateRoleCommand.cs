using Common.Application;
using User.Module.Data.Entities._Enum;

namespace UserModule.Core.Commands.Roles.Create;

public record CreateRoleCommand(string Title, List<Permission> Permissions) : IBaseCommand;