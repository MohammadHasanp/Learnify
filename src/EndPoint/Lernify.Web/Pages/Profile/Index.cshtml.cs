using Microsoft.AspNetCore.Mvc.RazorPages;
using Learnify.Web.Infrastructure.Util;
using UserModule.Core.Queries.Users.DTOs;
using UserModule.Core.Services;

namespace Learnify.Web.Pages.Profile
{
    public class IndexModel(IUserService service, IUserNotificationService userNotification) : PageModel
    {
        public UserDto? UserDto { get; set; } = new UserDto();
        public List<UserNotificationFilterData> NewNotifications { get; set; } = [];

        public async Task OnGet()
        {
            UserDto = await service.GetUserByMobile(User.GetUserMobile());
            var result = await userNotification.GetByFilter(new UserNotificationFilterParams()
            {
                IsSeen = false,
                PageId = 1,
                Take = 5,
                UserId = UserDto!.Id,
            });
            NewNotifications = result.Datas;
        }
    }
}
