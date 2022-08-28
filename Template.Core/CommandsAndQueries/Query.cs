using System;
using Template.Shared.Core.Messages;

namespace Template.Shared.Core.CommandsAndQueries
{
    public abstract class Query<TResponse> : Message, IQuery<TResponse>
    {
        protected Query() => Timestamp = DateTime.UtcNow;

        public string QueryId => Guid.NewGuid().ToString();
        public DateTime Timestamp { get; private set; }
    }
}
