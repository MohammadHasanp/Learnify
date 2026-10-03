using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketModule.Context;
using TicketModule.Services;

namespace TicketModule;

public static class TicketBootstrapper
{
    public static IServiceCollection InitTicketModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<TicketContext>(option => option.UseSqlServer(config.GetConnectionString("Ticket-Context")));
        services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());

        services.AddTransient<ITicketService, TicketService>();
        return services;
    }
}