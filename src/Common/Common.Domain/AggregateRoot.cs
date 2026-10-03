namespace Common.Domain;

using System.ComponentModel.DataAnnotations.Schema;

public class AggregateRoot : Entity
{
    private readonly List<DomainEvent> _baseDomainEvent = [];

    [NotMapped]
    public List<DomainEvent> BaseDomainEvent => this._baseDomainEvent;

    public void AddDomainEvent(DomainEvent baseDomain) => this._baseDomainEvent.Add(baseDomain);

    public void RemoveDomainEvent(DomainEvent baseDomain) => this._baseDomainEvent.Remove(baseDomain);
}
