using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Infra.Data.Extensions;
using Template.Shared.Core.Communication.Mediator;
using Template.Shared.Core.Data;
using Template.Shared.Core.DomainObjects;

namespace Template.Infra.Data.Contexts
{
    public class Context : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public Context(DbContextOptions<Context> options, IMediatorHandler mediatorHandler) 
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Entity>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }

        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;
            if (success) await _mediatorHandler.PublishEvents(ctx: this);

            return success;
        }
    }
}
