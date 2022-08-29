using System.Threading.Tasks;
using Template.Shared.Core.CommandsAndQueries;
using Template.Shared.Core.DomainEvents;
using Template.Shared.Core.Messages;
using Template.Shared.Core.Messages.Notifications;

namespace Template.Shared.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        // Commands
        Task<TResponse> SendCommand<TResponse>(ICommand<TResponse> command);
        Task<TResponse> SendQuery<TResponse>(IQuery<TResponse> query);
        // Exceptions/Notifications
        Task PublishNotification<T>(T notification) where T : DomainNotification;
        // Integration Events
        Task PublishEvent<T>(T evento) where T : Event;
        // Domain Events
        Task PublishDomainEvent<T>(T domainEvent) where T : DomainEvent;
    }
}
