using Template.Shared.Core.CommandsAndQueries;
using System.Threading;
using System.Threading.Tasks;
using Template.Domain.Repository;
using Template.Domain.DTOs;

namespace Template.Domain.Commands
{
    public class GetUserByIdQueriesHandler : IQueryHandler<GetUserByIdQueries, UserDTO>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueriesHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(GetUserByIdQueries command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(command.Id, cancellationToken);

            if (user is null) return null;

            return new UserDTO()
            {
                Id = user.Id,
                Name = user.Name
            };
        }
    }
}
