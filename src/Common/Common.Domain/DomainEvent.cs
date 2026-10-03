using MediatR;

namespace Common.Domain;

public class DomainEvent : INotification
{
    public DateTime CreationDate { get; private set; } = DateTime.Now;
}
