using Template.Shared.Core.CommandsAndQueries;
using Template.Domain.DTOs;
using System.Collections.Generic;

namespace Template.Domain.Commands
{
    public class GetUsersQueries : Query<IEnumerable<UserDTO>>
    {
        public GetUsersQueries()
        {
        }
    }
}
