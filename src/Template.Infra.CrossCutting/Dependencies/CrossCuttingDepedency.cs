using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Interfaces.CrossCutting;

namespace Template.Infra.CrossCutting
{
    public static class CrossCuttingDepedency
    {
        public static void AddInfraCrossCuttingModule(this IServiceCollection services)
        {
            services.AddScoped<IEnvironmentVariables, EnvironmentVariables>();
        }
    }
}
