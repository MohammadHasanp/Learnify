using Common.Application;
using Microsoft.EntityFrameworkCore;
using User.Module.Data.Context;

namespace UserModule.Core.Commands.Roles.Delete;

public class DeleteRoleHandler(UserContext context) : IBaseCommandHandler<DeleteRoleCommand>
{
    public async Task<OperationResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await context.Roles.Include(s => s.Permissions)
            .FirstOrDefaultAsync(f => f.Id == request.RoleId, cancellationToken);
        if (role == null)
        {
            return OperationResult.NotFound();
        }

        if (await UsersHaveThisRole(role.Id))
        {
            return OperationResult.Error("امکان حذف این نقش وجود ندارد");
        }
        context.Remove(role);
        await context.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }

    private async Task<bool> UsersHaveThisRole(Guid roleId)
    {
        return await context.UserRoles.AnyAsync(r => r.RoleId == roleId);
    }
}