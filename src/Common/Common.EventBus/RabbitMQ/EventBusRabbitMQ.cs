using Common.EventBus.Abstractions;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Common.EventBus.RabbitMQ;

public class EventBusRabbitMq(IConfiguration configuration, ILogger<EventBusRabbitMq> logger) : IEventBus
{
    private readonly string _hostName = configuration.GetSection("RabbitMQ")["HostName"]!;
    private readonly string _password = configuration.GetSection("RabbitMQ")["Password"]!;
    private readonly string _userName = configuration.GetSection("RabbitMQ")["UserName"]!;
    private IConnection? _connection;

    public async Task<IConnection> GetConnectionAsync()
    {
        await CreateRabbitMqConnection();
        return _connection!;
    }

    public async Task Publish(IntegrationEvent @event, string? queueName, string exchange = "", string exchangeType = ExchangeType.Direct, string routeKey = "")
    {
        var connection = await GetConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        if (string.IsNullOrWhiteSpace(routeKey))
        {
            routeKey = queueName ?? "";
        }

        if (!string.IsNullOrWhiteSpace(queueName))
        {
            await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            if (exchangeType == ExchangeType.Direct && exchange != "")
            {
                await channel.QueueBindAsync(queueName, exchange, queueName);
            }
        }

        if (!string.IsNullOrWhiteSpace(exchange))
        {
            await channel.ExchangeDeclareAsync(exchange, exchangeType, true, false);
        }

        var json = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties
        {
            Persistent = true
        };

        await channel.BasicPublishAsync(exchange, routeKey, false, properties, body);
    }

    private async Task CreateRabbitMqConnection()
    {
        try
        {
            if (_connection == null)
            {
                var factory = new ConnectionFactory()
                {
                    UserName = _userName,
                    Password = _password,
                    HostName = _hostName
                };
                _connection = await factory.CreateConnectionAsync();
            }
        }
        catch (Exception e)
        {
            logger.LogCritical(e.Message, e);
            throw;
        }
    }
}