using Template.Shared.Core.CommandsAndQueries;
using Template.Shared.Core.Communication.Mediator;
using System.Threading;
using System.Threading.Tasks;
using Template.Domain.Repository;
using Template.Domain.Resources;

namespace Template.Domain.Commands
{
    public class UpdateUserCommandHandler : CommandHandler, ICommandHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IMediatorHandler mediatorHandler,
            IUserRepository userRepository) : base(mediatorHandler)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(command.Id, cancellationToken);
            if (user is null)
            {
                await AddNotification(nameof(UpdateUserCommand), Messages.UserNotFound);
                return false;
            }

            user.UpdateName(command.Name, nameof(UpdateUserCommand));

            return await _userRepository.UnitOfWork.Commit();
        }
    }
}
