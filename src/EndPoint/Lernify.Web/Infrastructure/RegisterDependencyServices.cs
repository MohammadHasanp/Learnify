using Common.EventBus.Abstractions;
using Common.EventBus.RabbitMQ;
using Learnify.Web.Infrastructure.RazorUtil;
using Learnify.Web.Infrastructure.Services;

namespace Learnify.Web.Infrastructure
{
    public static class RegisterDependencyServices
    {
        public static IServiceCollection RegisterApiServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandlers>();
            services.AddScoped<IRenderViewToString, RenderViewToString>();
            services.AddSingleton<IEventBus, EventBusRabbitMq>();

            services.AddAutoMapper(a =>
            {
                a.AddProfile<MapperProfile>();
            });

            services.AddHttpContextAccessor();
            services.AddScoped<IHomePageService, HomePageService>();
            return services;
        }
    }
}
