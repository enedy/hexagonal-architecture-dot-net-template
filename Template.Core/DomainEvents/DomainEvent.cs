using System;
using Template.Shared.Core.Messages;

namespace Template.Shared.Core.DomainEvents
{
    public class DomainEvent : Event
    {
        public Guid AggregateId { get; private set; }
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
