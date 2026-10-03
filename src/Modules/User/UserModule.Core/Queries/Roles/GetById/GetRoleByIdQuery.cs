using Common.Query;
using UserModule.Core.Queries.Roles.DTOs;

namespace UserModule.Core.Queries.Roles.GetById;

public record GetRoleByIdQuery(Guid RoleId) : IQuery<RoleDto?>;