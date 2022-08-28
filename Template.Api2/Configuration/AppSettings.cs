namespace Template.Api.Configuration
{
    public class AppSettings
    {
        public AppSettings()
        {
            Secret = "fedaf7d8863b48e197b9287d492b708e";
            Expiration = 12;
            Emmiter = "AFD";
            ValidIn = "https://localhost";
        }

        public string Secret { get; private set; }
        public int Expiration { get; private set; }
        public string Emmiter { get; private set; }
        public string ValidIn { get; private set; }
    }
}
