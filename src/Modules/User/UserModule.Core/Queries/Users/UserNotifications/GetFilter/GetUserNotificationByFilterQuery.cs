using Common.Query;
using UserModule.Core.Queries.Users.DTOs;

namespace UserModule.Core.Queries.Users.UserNotifications.GetFilter;

public class GetUserNotificationByFilterQuery : QueryFilter<UserNotificationFilterResult, UserNotificationFilterParams>
{
    public GetUserNotificationByFilterQuery(UserNotificationFilterParams filterParams) : base(filterParams)
    {
    }
}
