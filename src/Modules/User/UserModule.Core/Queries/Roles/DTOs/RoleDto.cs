using Common.Query;
using User.Module.Data.Entities._Enum;

namespace UserModule.Core.Queries.Roles.DTOs;

public class RoleDto : BaseDto
{
    public string RoleTitle { get; set; } = null!;
    public List<Permission> Permissions { get; set; } = [];
}