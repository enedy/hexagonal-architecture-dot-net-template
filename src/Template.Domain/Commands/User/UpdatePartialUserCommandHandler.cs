using Template.Shared.Core.CommandsAndQueries;
using Template.Shared.Core.Communication.Mediator;
using System.Threading;
using System.Threading.Tasks;
using Template.Domain.Repository;
using Template.Domain.Resources;

namespace Template.Domain.Commands
{
    public class UpdatePartialUserCommandHandler : CommandHandler, ICommandHandler<UpdatePartialUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public UpdatePartialUserCommandHandler(IMediatorHandler mediatorHandler,
            IUserRepository userRepository) : base(mediatorHandler)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdatePartialUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(command.Id, cancellationToken);
            if (user is null)
            {
                await AddNotification(nameof(UpdatePartialUserCommand), Messages.UserNotFound);
                return false;
            }

            command.User.ApplyTo(user);

            return await _userRepository.UnitOfWork.Commit();
        }
    }
}
