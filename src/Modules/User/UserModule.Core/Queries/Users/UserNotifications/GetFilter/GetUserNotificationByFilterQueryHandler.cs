using Common.Query;
using Microsoft.EntityFrameworkCore;
using User.Module.Data.Context;
using UserModule.Core.Queries.Users.DTOs;

namespace UserModule.Core.Queries.Users.UserNotifications.GetFilter;

public class GetUserNotificationByFilterQueryHandler(UserContext context) : IQueryHandler<GetUserNotificationByFilterQuery, UserNotificationFilterResult>
{
    private readonly UserContext _context = context;
    public async Task<UserNotificationFilterResult> Handle(GetUserNotificationByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _context.Notifications.Where(n => n.UserId == @params.UserId).AsQueryable();

        if (@params.IsSeen != null)
            result = result.Where(n => n.IsSeen == @params.IsSeen);

        var skip = (@params.PageId - 1) * @params.Take;
        var model = new UserNotificationFilterResult()
        {
            Datas = await result.Skip(skip).Take(@params.Take).Select(n => new UserNotificationFilterData()
            {
                CreationDate = n.CreationDate,
                Id = n.Id,
                IsDelete = n.IsDelete,
                IsSeen = n.IsSeen,
                Text = n.Text,
                Title = n.Title,
                UserId = n.UserId
            }).ToListAsync(cancellationToken: cancellationToken)
        };

        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }
}