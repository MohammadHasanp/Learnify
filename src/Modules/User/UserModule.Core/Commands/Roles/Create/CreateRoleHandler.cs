using Common.Application;
using Microsoft.EntityFrameworkCore;
using User.Module.Data.Context;
using User.Module.Data.Entities.Roles;

namespace UserModule.Core.Commands.Roles.Create;

public class CreateRoleHandler(UserContext context) : IBaseCommandHandler<CreateRoleCommand>
{
    public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        if (!request.Permissions.Any())
        {
            return OperationResult.Error("برای نقش مورد نظر دسترسی مشخص کنید");
        }

        if (await context.Roles.AnyAsync(s => s.Name == request.Title, cancellationToken: cancellationToken))
        {
            return OperationResult.Error("نقش مورد نظر در سیستم موجود است");
        }

        var role = new Role
        {
            Name = request.Title,
        };
        context.Roles.Add(role);

        foreach (var permission in request.Permissions)
        {
            context.RolePermissions.Add(new RolePermission()
            {
                Permission = permission,
                RoleId = role.Id
            });
        }

        await context.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}