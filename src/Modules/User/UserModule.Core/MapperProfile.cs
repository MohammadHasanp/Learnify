using AutoMapper;
using User.Module.Data.Entities.UserNotifications;
using UserModule.Core.Commands.Users.UserNotifications.Create;
using UserModule.Core.Queries.Users.DTOs;
using User.Module.Data.Entities.Users;

namespace UserModule.Core;

class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserDto,User.Module.Data.Entities.Users.User>().ReverseMap();
        CreateMap<CreateUserNotificationCommand, UserNotification>().ReverseMap();
    }
}
