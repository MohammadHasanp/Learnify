using Common.Query;
using Microsoft.EntityFrameworkCore;
using User.Module.Data.Context;
using UserModule.Core.Queries.Roles.DTOs;

namespace UserModule.Core.Queries.Roles.GetById;

public class GetRoleByIdHandler(UserContext context) : IQueryHandler<GetRoleByIdQuery, RoleDto?>

{
    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await context.Roles.Include(d => d.Permissions)
            .FirstOrDefaultAsync(d => d.Id == request.RoleId, cancellationToken);

        if (role == null)
            return null;

        return new RoleDto()
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            RoleTitle = role.Name,
            Permissions = role.Permissions.Select(d => d.Permission).ToList()
        };
    }
}