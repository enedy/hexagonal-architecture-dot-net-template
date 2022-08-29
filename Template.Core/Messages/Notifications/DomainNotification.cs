using System;
using MediatR;

namespace Template.Shared.Core.Messages.Notifications
{
    public class DomainNotification : Message, INotification
    {
        public DateTime Timestamp { get; private set; }
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }

        public DomainNotification(string key, string value)
        {
            Timestamp = DateTime.UtcNow;
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
        }
    }
}
