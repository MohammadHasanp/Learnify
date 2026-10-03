using Common.Domain;

namespace CoreModule.Domain.Orders.Events;

public class OrderFinallyEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
}