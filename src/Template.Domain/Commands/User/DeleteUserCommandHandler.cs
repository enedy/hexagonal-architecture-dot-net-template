using Template.Shared.Core.CommandsAndQueries;
using Template.Shared.Core.Communication.Mediator;
using System.Threading;
using System.Threading.Tasks;
using Template.Domain.Interfaces.Repository;
using Template.Domain.Resources;

namespace Template.Domain.Commands
{
    public class DeleteUserCommandHandler : CommandHandler, ICommandHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IMediatorHandler mediatorHandler,
            IUserRepository userRepository) : base(mediatorHandler)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(command.Id, cancellationToken);
            if (user is null)
            {
                await AddNotification(nameof(DeleteUserCommand), Messages.UserNotFound);
                return false;
            }

            _userRepository.DeleteUserAsync(user);
            return await _userRepository.UnitOfWork.Commit();
        }
    }
}
