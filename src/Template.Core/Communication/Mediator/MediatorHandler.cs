using System.Threading.Tasks;
using MediatR;
using Template.Shared.Core.CommandsAndQueries;
using Template.Shared.Core.DomainEvents;
using Template.Shared.Core.Messages;
using Template.Shared.Core.Messages.Notifications;

namespace Template.Shared.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        public MediatorHandler(IMediator mediator) => _mediator = mediator;

        public async Task<TResponse> SendCommand<TResponse>(ICommand<TResponse> command) => await _mediator.Send(command);
        public async Task PublishNotification<T>(T notification) where T : DomainNotification => await _mediator.Publish(notification);
        public async Task PublishDomainEvent<T>(T domainEvent) where T : DomainEvent => await _mediator.Publish(domainEvent);
        public async Task PublishEvent<T>(T evento) where T : Event => await _mediator.Publish(evento);
        public async Task<TResponse> SendQuery<TResponse>(IQuery<TResponse> query) => await _mediator.Send(query);
    }
}
