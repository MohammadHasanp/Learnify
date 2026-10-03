using AutoMapper;
using Common.Application;
using User.Module.Data.Context;
using User.Module.Data.Entities.UserNotifications;


namespace UserModule.Core.Commands.Users.UserNotifications.Create;

public class CreateUserNotificationHandler(IMapper mapper, UserContext userContext) : IBaseCommandHandler<CreateUserNotificationCommand>
{
    private readonly UserContext _context = userContext;
    private readonly IMapper _mapper = mapper;
    public async Task<OperationResult> Handle(CreateUserNotificationCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<UserNotification>(request);
        _context.Notifications.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}
