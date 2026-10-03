using Common.Query;

namespace UserModule.Core.Queries.Users.DTOs;

public class UserNotificationFilterResult : BaseFilter<UserNotificationFilterData>
{
}


public class UserNotificationFilterParams : BaseFilterParam
{
    public Guid UserId { get; set; }
    public bool? IsSeen { get; set; }
}


public class UserNotificationFilterData : BaseDto
{
    public string Text { get; set; } = null!;
    public string Title { get; set; } = null!;
    public Guid UserId { get; set; }
    public bool IsSeen { get; set; }

}
