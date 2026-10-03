using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionModule.Context;
using TransactionModule.Services;
using TransactionModule.Services.Zarinpal;

namespace TransactionModule;

public static class TransactionBootstrapper
{
    public static IServiceCollection InitTransactionModule(this IServiceCollection service, IConfiguration config)
    {
        service.AddDbContext<TransactionContext>(option =>
        {
            option.UseSqlServer(config.GetConnectionString("Transaction-Context"));
        });
        service.AddTransient<ITransactionService, TransactionService>();
        service.AddTransient<IZarinPalService, ZarinPalService>();
        return service;
    }
}