using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
using User.Module.Data.Context;

namespace UserModule.Core.Commands.Users.ChangePassword;

public class ChangeUserPasswordHandler(UserContext context) : IBaseCommandHandler<ChangeUserPasswordCommand>
{
    public async Task<OperationResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);
        if (user == null)
            return OperationResult.NotFound();

        if (Sha256Hasher.IsCompare(user.Password, request.CurrentPasssword))
        {
            user.Password = Sha256Hasher.Hash(request.NewPassword);
            context.Update(user);
            await context.SaveChangesAsync(cancellationToken);
            return OperationResult.Success();
        }

        else
            return OperationResult.Error("کلمه عبور نامعتبر است");
    }
}
