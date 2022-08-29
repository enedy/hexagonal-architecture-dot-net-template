using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Repository;
using Template.Infra.Data.Contexts;
using Template.Infra.Data.Repository;

namespace Template.Infra.Data.Dependencies
{
    public static class ContextModuleDependency
    {
        public static void AddDataBaseModule(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Context>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
