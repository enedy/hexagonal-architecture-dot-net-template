using Template.Shared.Core.CommandsAndQueries;
using Template.Domain.DTOs;
using System.Collections.Generic;
using System;

namespace Template.Domain.Commands
{
    public class GetUserByIdQueries : Query<UserDTO>
    {
        public Guid Id { get; private set; }
        public GetUserByIdQueries(Guid id)
        {
            Id = id;
        }
    }
}
