using Common.Query;
using UserModule.Core.Queries.Roles.DTOs;

namespace UserModule.Core.Queries.Roles.GetAll;

public record GetAllRoleQuery : IQuery<List<RoleDto>>;

