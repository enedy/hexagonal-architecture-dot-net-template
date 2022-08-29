using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Template.Domain.Events
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        public UserCreatedEventHandler()
        {
        }

        public async Task Handle(UserCreatedEvent mensagem, CancellationToken cancellationToken)
        {
            var id = mensagem.AggregateId;

            // SEND A E-MAIL
        }
    }
}
