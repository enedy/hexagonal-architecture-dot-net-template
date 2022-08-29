using Template.Shared.Core.CommandsAndQueries;
using System.Threading;
using System.Threading.Tasks;
using Template.Domain.Repository;
using Template.Domain.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Template.Domain.Commands
{
    public class GetUsersQueriesHandler : IQueryHandler<GetUsersQueries, IEnumerable<UserDTO>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueriesHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> Handle(GetUsersQueries command, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersAsync(cancellationToken);

            if (!users.Any()) return null;

            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name
            })
            .ToList();
        }
    }
}
