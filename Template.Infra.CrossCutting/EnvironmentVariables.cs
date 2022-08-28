namespace Template.Infra.CrossCutting
{
    public class EnvironmentVariables
    {
        public EnvironmentVariables()
        {
            CreateConnectionStrings();
        }

        public string ConnectionString { get; private set; }
        private void CreateConnectionStrings()
        {
            ConnectionString = Environment.GetEnvironmentVariable("ConnectionString");
        }
    }
}
