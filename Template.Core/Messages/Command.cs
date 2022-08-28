using System;
using FluentValidation.Results;
using Template.Shared.Core.CommandsAndQueries;

namespace Template.Shared.Core.Messages
{
    public abstract class Command<TResponse> : Message, ICommand<TResponse>
    {
        protected Command() => Timestamp = DateTime.UtcNow;

        public string CommandId => Guid.NewGuid().ToString();
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }
        public virtual bool IsValid() => throw new NotImplementedException();
    }
}
