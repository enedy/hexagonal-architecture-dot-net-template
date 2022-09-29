using Template.Shared.Core.Data;
using System.Threading;
using System.Threading.Tasks;
using Template.Domain.Entities;
using System.Collections.Generic;
using System;

namespace Template.Domain.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken);
        Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        void DeleteUserAsync(User user);
        Task SaveUserAsync(User user, CancellationToken cancellationToken);
    }
}
