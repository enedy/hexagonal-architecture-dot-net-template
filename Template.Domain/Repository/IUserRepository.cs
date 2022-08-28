using Template.Shared.Core.Data;
using System.Threading;
using System.Threading.Tasks;
using Template.Domain.Entities;
using System.Collections.Generic;

namespace Template.Domain.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken);
        Task SaveUserAsync(User user, CancellationToken cancellationToken);
    }
}
