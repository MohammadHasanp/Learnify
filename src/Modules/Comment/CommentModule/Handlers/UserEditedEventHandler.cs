using System.Text;
using CommentModule.Context;
using CommentModule.Domain;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommentModule.Handlers;

public class UserEditedEventHandler(IServiceProvider provider, IServiceScopeFactory scopeFactory, ILogger<UserEditedEventHandler> logger) : BackgroundService
{
    private const string QueueName = "commentModuleUserEdited";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var eventBus = provider.GetRequiredService<IEventBus>();
        var connection = await eventBus.GetConnectionAsync();
        var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await channel.ExchangeDeclareAsync(Exchanges.UserTopicExchange, ExchangeType.Topic, true, false, cancellationToken: stoppingToken);
        await channel.QueueDeclareAsync(QueueName, true, false, false, cancellationToken: stoppingToken);
        await channel.QueueBindAsync(QueueName, Exchanges.UserTopicExchange, "user.edited", cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (_, args) =>
        {
            using var serviceFactory = scopeFactory.CreateScope();
            var context = serviceFactory.ServiceProvider.GetRequiredService<CommentContext>();
            try
            {
                var userJson = Encoding.UTF8.GetString(args.Body.ToArray());
                var user = JsonConvert.DeserializeObject<UserEdited>(userJson);
                var oldUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user!.UserId, stoppingToken);
                if (oldUser == null)
                {
                    context.Users.Add(new User
                    {
                        Name = user!.Name,
                        Family = user.Family,
                        Email = user.Email,
                        AvatarName = "avatar.png",
                        Id = user.UserId,
                        CreationDate = user.CreationDate,
                        IsDelete = false,
                    });
                }
                else
                {
                    oldUser.Email = user!.Email;
                    oldUser.Name = user.Name;
                    oldUser.Family = user.Family;
                    context.Update(oldUser);
                }

                await context.SaveChangesAsync(stoppingToken);
                await channel.BasicAckAsync(args.DeliveryTag, false, stoppingToken);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                await channel.BasicNackAsync(args.DeliveryTag, false, requeue: false, stoppingToken);
            }
        };

        await channel.BasicConsumeAsync(QueueName, false, consumer, cancellationToken: stoppingToken);
    }
}