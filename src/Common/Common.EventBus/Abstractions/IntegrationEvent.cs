namespace Common.EventBus.Abstractions;

public class IntegrationEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime CreationDate { get; } = DateTime.Now;
}