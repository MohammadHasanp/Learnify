using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Module.Data.Context;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.EventHandlers;
using UserModule.Core.Queries.Users.DTOs;
using UserModule.Core.Services;
namespace UserModule.Core;

public static class UserModuleBootstrapper
{
    public static IServiceCollection InitUserModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<UserContext>(option => option.UseSqlServer(config.GetConnectionString("User-Context")));
        services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());

        services.AddHostedService<NewNotificationEventHandler>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserNotificationService, UserNotificationService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UserDto>());
        services.AddValidatorsFromAssembly(typeof(RegisterUserValidator).Assembly);
        return services;
    }
}
