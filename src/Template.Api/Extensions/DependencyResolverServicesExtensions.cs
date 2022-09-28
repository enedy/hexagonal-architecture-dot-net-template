using Template.Shared.Core.Communication.Mediator;
using Template.Shared.Core.Messages.Notifications;
using Template.Shared.Core.Messages.Notifications.Mediator;
using MediatR;
using Template.Domain.Dependencies;
using Template.Infra.CrossCutting;
using Template.Infra.Data.Dependencies;

namespace Template.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependencyResolverServicesExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddMediatR(typeof(DependencyResolverServicesExtensions));
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // INFRA
            services.AddInfraCrossCuttingModule();

            var environmentVariables = services.BuildServiceProvider().GetService<IEnvironmentVariables>();

            // Repository
            services.AddDataBaseModule(environmentVariables.ConnectionString);

            // Domain
            services.AddDomainModule();
        }
    }
}
