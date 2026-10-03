using CommentModule.Context;
using CommentModule.Handlers;
using CommentModule.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommentModule;

public static class CommentBootstrapper
{
    public static IServiceCollection InitCommentModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<CommentContext>(option => option.UseSqlServer(config.GetConnectionString("Comment-Context")));
        services.AddAutoMapper(cfg => cfg.AddProfile<CommentProfile>());

        services.AddScoped<ICommentService, CommentService>();
        services.AddHostedService<UserEditedEventHandler>();
        services.AddHostedService<UserRegisteredEventHandler>();
        return services;
    }
}