using Common.Application;
using User.Module.Data.Entities._Enum;

namespace UserModule.Core.Commands.Roles.Edit;

public record EditRoleCommand(Guid RoleId, List<Permission> Permissions, string Title) : IBaseCommand;