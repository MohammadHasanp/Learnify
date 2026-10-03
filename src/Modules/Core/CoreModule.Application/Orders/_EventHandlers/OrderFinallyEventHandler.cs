using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Domain.Orders.Events;
using MediatR;
using RabbitMQ.Client;

namespace CoreModule.Application.Orders._EventHandlers;

public class OrderFinallyEventHandler(IEventBus eventBus) : INotificationHandler<OrderFinallyEvent>
{
    public async Task Handle(OrderFinallyEvent notification, CancellationToken cancellationToken)
    {
        await eventBus.Publish(new NewNotificationIntegrationEvent()
        {
            Description = "فاکتور شما با موفقیت پرداخت شد",
            Title = "پرداخت موفق",
            UserId = notification.UserId
        }, null, Exchanges.NotificationExchange, ExchangeType.Fanout);
        await Task.CompletedTask;
    }
}