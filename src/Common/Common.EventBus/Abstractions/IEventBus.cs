using RabbitMQ.Client;

namespace Common.EventBus.Abstractions;

public interface IEventBus
{
    public Task<IConnection> GetConnectionAsync();
    public Task Publish(IntegrationEvent @event, string? queueName = null, string exchange = "", string exchangeType = ExchangeType.Direct, string routeKey = "");
}