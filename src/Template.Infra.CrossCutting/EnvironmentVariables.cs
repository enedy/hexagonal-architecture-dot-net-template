using Microsoft.Extensions.Configuration;
using Template.Domain.Interfaces.CrossCutting;

namespace Template.Infra.CrossCutting
{
    public class EnvironmentVariables : IEnvironmentVariables
    {
        private readonly IConfiguration _configuration;

        public EnvironmentVariables(IConfiguration configuration)
        {
            _configuration = configuration;
            CreateConnectionStrings();
        }

        public string ConnectionString { get; private set; }
        private void CreateConnectionStrings() => ConnectionString = _configuration["ConnectionString"];
    }
}
