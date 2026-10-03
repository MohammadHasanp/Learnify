using System.Text;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using User.Module.Data.Context;
using User.Module.Data.Entities.UserNotifications;

namespace UserModule.Core.EventHandlers;

public class NewNotificationEventHandler(IEventBus eventBus, IServiceScopeFactory scopeFactory, ILogger<NewNotificationEventHandler> logger)
    : BackgroundService
{
    private readonly string _queueName = "userNotificationHandler";
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connection = await eventBus.GetConnectionAsync();
        var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
        var serviceFactory = scopeFactory.CreateScope();
        var context = serviceFactory.ServiceProvider.GetRequiredService<UserContext>();

        await channel.ExchangeDeclareAsync(Exchanges.NotificationExchange, ExchangeType.Fanout, true, false, cancellationToken: stoppingToken);
        await channel.QueueDeclareAsync(_queueName, true, false, false, cancellationToken: stoppingToken);
        await channel.QueueBindAsync(_queueName, Exchanges.NotificationExchange, "", cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (_, args) =>
        {
            try
            {
                var eventJson = Encoding.UTF8.GetString(args.Body.ToArray());
                var notification = JsonConvert.DeserializeObject<NewNotificationIntegrationEvent>(eventJson);

                context.Notifications.Add(new UserNotification()
                {
                    Title = notification!.Title,
                    Text = notification.Description,
                    UserId = notification.UserId,
                    CreationDate = notification.CreationDate,
                    IsDelete = false
                });

                await context.SaveChangesAsync(stoppingToken);
                await channel.BasicAckAsync(args.DeliveryTag, false, stoppingToken);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
            }
        };

        await channel.BasicConsumeAsync(_queueName, false, consumer, cancellationToken: stoppingToken);
    }
}