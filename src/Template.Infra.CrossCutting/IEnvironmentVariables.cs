namespace Template.Infra.CrossCutting
{
    public interface IEnvironmentVariables
    {
        string ConnectionString { get; }
    }
}
