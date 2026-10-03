using Common.Application;
using Common.Application.SecurityUtil;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using User.Module.Data.Context;
using User.Module.Data.Entities.Users;

namespace UserModule.Core.Commands.Users.FullEdit;

public class FullEditUserCommand : IBaseCommand
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Family { get; set; }
    public string? Email { get; set; }
    public string Mobile { get; set; } = null!;
    public string? Password { get; set; }
    public List<Guid> Roles { get; set; } = [];
}
public class FullEditUserCommandHandler(UserContext context, IEventBus eventBus) : IBaseCommandHandler<FullEditUserCommand>
{
    public async Task<OperationResult> Handle(FullEditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Include(c => c.UserRoles)
            .FirstOrDefaultAsync(f => f.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return OperationResult.NotFound();
        }
        if (request.Mobile != user.Mobile &&
            await MobileIsDuplicated(request.Mobile))
        {
            return OperationResult.Error("شماره تلفن تکراری است");
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            if (request.Email.ToLower() != user.Email &&
                await EmailIsDuplicated(request.Email))
            {
                return OperationResult.Error("ایمیل وارد شده تکراری است");
            }
            user.Email = request.Email.ToLower();
        }

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            user.Password = Sha256Hasher.Hash(request.Password);
        }
        user.Name = request.Name;
        user.Family = request.Family;
        user.Mobile = request.Mobile;

        context.UserRoles.RemoveRange(user.UserRoles);
        var userRoles = new List<UserRole>();
        foreach (var roleId in request.Roles)
        {
            userRoles.Add(new UserRole()
            {
                RoleId = roleId,
                UserId = user.Id
            });
        }

        context.Users.Update(user);
        context.UserRoles.AddRange(userRoles);
        await context.SaveChangesAsync(cancellationToken);
        await eventBus.Publish(new UserEdited()
        {
            Email = user.Email,
            Family = user.Family,
            Name = user.Name,
            UserId = user.Id,
            Mobile = user.Mobile
        }, null, Exchanges.UserTopicExchange, ExchangeType.Topic, "user.edited");

        return OperationResult.Success();
    }
    private async Task<bool> EmailIsDuplicated(string email)
    {
        return await context.Users.AnyAsync(f => f.Email == email.ToLower());
    }
    private async Task<bool> MobileIsDuplicated(string mobile)
    {
        return await context.Users.AnyAsync(f => f.Mobile == mobile);
    }
}