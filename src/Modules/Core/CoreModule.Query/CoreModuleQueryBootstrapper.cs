using CoreModule.Query._Data;
using CoreModule.Query._Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreModule.Query;

public static class CoreModuleQueryBootstrapper
{
    public static IServiceCollection RegisterDependency(this IServiceCollection services, IConfiguration config)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<UserQueryModel>();
        });
        services.AddDbContext<QueryContext>(option =>
        {
            option.UseSqlServer(config.GetConnectionString("Core-Context"));
        });
        return services;
    }
}
