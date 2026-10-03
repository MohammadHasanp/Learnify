using Common.Application;
using Microsoft.EntityFrameworkCore;
using User.Module.Data.Context;
using User.Module.Data.Entities.Roles;

namespace UserModule.Core.Commands.Roles.Edit;

public class EditRoleHandler(UserContext context) : IBaseCommandHandler<EditRoleCommand>
{
    public async Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        if (request.Permissions.Count == 0)
        {
            return OperationResult.Error("لطفا دسترسی ها را مشخص کنید");
        }

        var role = await context.Roles.Include(d => d.Permissions)
            .FirstOrDefaultAsync(s => s.Id == request.RoleId, cancellationToken: cancellationToken);
        if (role == null)
            return OperationResult.NotFound();


        if (role.Name != request.Title)
        {
            var roleIsExist = await context.Roles.AnyAsync(f => f.Name == request.Title, cancellationToken: cancellationToken);
            if (roleIsExist)
            {
                return OperationResult.Error("این نقش قبلا ساخته شده است");
            }
        }


        role.Name = request.Title;
        context.Roles.Update(role);

        context.RolePermissions.RemoveRange(role.Permissions);
        foreach (var permission in request.Permissions)
        {
            context.RolePermissions.Add(new RolePermission()
            {
                RoleId = role.Id,
                Permission = permission
            });
        }
        await context.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();

    }
}