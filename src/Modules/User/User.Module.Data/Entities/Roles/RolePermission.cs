using Common.Domain;
using User.Module.Data.Entities._Enum;

namespace User.Module.Data.Entities.Roles;

public class RolePermission : Entity
{
    public Guid RoleId { get; set; }
    public Permission Permission { get; set; }
}
