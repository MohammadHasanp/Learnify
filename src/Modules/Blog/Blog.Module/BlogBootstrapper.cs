namespace BlogModule;

using BlogModule.Context;
using BlogModule.Repositories.Categories;
using BlogModule.Repositories.Posts;
using BlogModule.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class BlogBootstrapper
{
    public static IServiceCollection InitBlogModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<BlogContext>(option => option.UseSqlServer(config.GetConnectionString("Blog-Context")));
        services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());

        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IBlogService, BlogService>();

        return services;
    }
}
