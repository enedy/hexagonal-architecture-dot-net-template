using Template.Shared.Core.CommandsAndQueries;
using Template.Shared.Core.Communication.Mediator;
using System.Threading;
using System.Threading.Tasks;
using Template.Domain.Entities;
using Template.Domain.Events;
using Template.Domain.Interfaces.Repository;
using System;

namespace Template.Domain.Commands
{
    public class AddUserCommandHandler : CommandHandler, ICommandHandler<AddUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public AddUserCommandHandler(IMediatorHandler mediatorHandler,
            IUserRepository userRepository) : base(mediatorHandler)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User(command.Name);
            user.AddEvent(new UserCreatedEvent(user.Id));

            await this.ValidateCommand(command);

            await _userRepository.SaveUserAsync(user, cancellationToken);

            await _userRepository.UnitOfWork.Commit();

            return user.Id;
        }
    }
}
