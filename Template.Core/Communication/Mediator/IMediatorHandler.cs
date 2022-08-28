using System.Threading.Tasks;
using Template.Shared.Core.CommandsAndQueries;
using Template.Shared.Core.DomainEvents;
using Template.Shared.Core.Messages;
using Template.Shared.Core.Messages.Notifications;

namespace Template.Shared.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        // COMMANDS
        Task<TResponse> SendCommand<TResponse>(ICommand<TResponse> command);
        Task<TResponse> SendQuery<TResponse>(IQuery<TResponse> query);
        // EXCEPTIONS/NOTIFICATIONS
        Task PublishNotification<T>(T notification) where T : DomainNotification;
        // INTEGRATION EVENTS
        Task PublishEvent<T>(T evento) where T : Event;
        // DOMAIN EVENTS
        Task PublishDomainEvent<T>(T domainEvent) where T : DomainEvent;
    }
}
