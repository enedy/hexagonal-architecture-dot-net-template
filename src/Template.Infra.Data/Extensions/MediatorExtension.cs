using Microsoft.EntityFrameworkCore;
using Template.Shared.Core.Communication.Mediator;
using Template.Shared.Core.DomainObjects;

namespace Template.Infra.Data.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublishEvents(this IMediatorHandler mediator, DbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
