using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Shared.Core.Messages
{
    public abstract class Message
    {
        [NotMapped]
        public string MessageType { get; protected set; }

        public Message()
        {
            MessageType = GetType().Name;
        }
    }
}
