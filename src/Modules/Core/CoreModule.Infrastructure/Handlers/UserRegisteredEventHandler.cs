using System.Text;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Infrastructure.Persistent._Context;
using CoreModule.Infrastructure.Persistent.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CoreModule.Infrastructure.Handlers;

public class UserRegisteredEventHandler(IServiceProvider provider, IServiceScopeFactory scopeFactory,
    ILogger<UserRegisteredEventHandler> logger) : BackgroundService
{
    private const string QueueName = "CoreModuleUserRegistred";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var eventbus = provider.GetRequiredService<IEventBus>();
        var connection = await eventbus.GetConnectionAsync();
        var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
        var serviceFactory = scopeFactory.CreateScope();
        var context = serviceFactory.ServiceProvider.GetRequiredService<CoreModuleEfContext>();

        await channel.ExchangeDeclareAsync(Exchanges.UserTopicExchange, ExchangeType.Topic, true, false, null, cancellationToken: stoppingToken);
        await channel.QueueDeclareAsync(QueueName, true, false, false, null, cancellationToken: stoppingToken);
        await channel.QueueBindAsync(QueueName, Exchanges.UserTopicExchange, "user.register", cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (sender, args) =>
        {
            try
            {
                var userJson = Encoding.UTF8.GetString(args.Body.ToArray());
                var user = JsonConvert.DeserializeObject<UserRegistered>(userJson);

                context.Users.Add(new User
                {
                    Id = user!.Id,
                    Name = user.Name,
                    Family = user.Family,
                    Mobile = user.Mobile,
                    Email = user.Email,
                    Avatar = user.Avatar,
                    Password = user.Password,
                });
                await context.SaveChangesAsync(stoppingToken);
                await channel.BasicAckAsync(args.DeliveryTag, false, stoppingToken);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
            }
        };

        await channel.BasicConsumeAsync(QueueName, false, consumer, cancellationToken: stoppingToken);
    }
}