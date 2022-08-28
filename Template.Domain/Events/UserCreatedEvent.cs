using System;
using Template.Shared.Core.DomainEvents;

namespace Template.Domain.Events
{
    public class UserCreatedEvent : DomainEvent
    {
        public Guid UserId { get; private set; }

        public UserCreatedEvent(Guid userId) : base(userId)
        {
            UserId = userId;
        }
    }
}
