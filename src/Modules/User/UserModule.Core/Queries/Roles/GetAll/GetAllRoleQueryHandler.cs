using Common.Query;
using Microsoft.EntityFrameworkCore;
using User.Module.Data.Context;
using UserModule.Core.Queries.Roles.DTOs;

namespace UserModule.Core.Queries.Roles.GetAll;

public class GetAllRoleQueryHandler(UserContext context) : IQueryHandler<GetAllRoleQuery, List<RoleDto>>
{
    public async Task<List<RoleDto>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        var roles = await context.Roles.Include(d => d.Permissions)
            .OrderByDescending(d => d.CreationDate).ToListAsync(cancellationToken: cancellationToken);

        var rolesDto = new List<RoleDto>();

        foreach (var role in roles)
        {
            rolesDto.Add(new RoleDto()
            {
                Permissions = role.Permissions.Select(s => s.Permission).ToList(),
                RoleTitle = role.Name,
                Id = role.Id,
                CreationDate = role.CreationDate
            });
        }
        return rolesDto;
    }
}