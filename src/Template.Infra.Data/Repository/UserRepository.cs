using Microsoft.EntityFrameworkCore;
using Template.Infra.Data.Contexts;
using Template.Shared.Core.Data;
using Template.Domain.Entities;
using Template.Domain.Interfaces.Repository;

namespace Template.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context oracleContext) => _context = oracleContext;

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken)
        {
            return await _context.User.ToListAsync(cancellationToken);
        }

        public async Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.User.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
        }

        public void DeleteUserAsync(User user)
        {
            _context.User.Remove(user);
        }

        public async Task SaveUserAsync(User user, CancellationToken cancellationToken)
        {
            await _context.User.AddAsync(user, cancellationToken);
        }

        public void Dispose() => _context?.Dispose();
    }
}
