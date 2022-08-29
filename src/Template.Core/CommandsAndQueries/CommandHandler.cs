using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Shared.Core.Communication.Mediator;
using Template.Shared.Core.Messages;
using Template.Shared.Core.Messages.Notifications;

namespace Template.Shared.Core.CommandsAndQueries
{
    public abstract class CommandHandler
    {
        public readonly IMediatorHandler _mediatorHandler;
        public CommandHandler(IMediatorHandler mediatorHandler) => _mediatorHandler = mediatorHandler;

        public async Task<bool> ValidateCommand<TResponse>(Command<TResponse> command)
        {
            if (command.IsValid()) return true;

            await this.AddNotifications(command.ValidationResult.Errors);

            return false;
        }

        private async Task AddNotifications(IList<ValidationFailure> errors)
        {
            foreach (var error in errors)
                await AddNotification(error.ErrorCode, error.ErrorMessage);
        }

        public async Task AddNotification(string key, string message)
        {
            await _mediatorHandler.PublishNotification(new DomainNotification(key, message));
        }
    }
}
