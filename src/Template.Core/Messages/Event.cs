using MediatR;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Shared.Core.Messages
{
    public abstract class Event : Message, INotification
    {
        [NotMapped]
        public DateTime TimeStamp { get; private set; }

        public Event() => TimeStamp = DateTime.UtcNow;
    }
}
