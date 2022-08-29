namespace Template.Api.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class AppConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public AppConfiguration()
        {
            Secret = "f78fb36c-413f-491c-909c-62b3b0ab01d5";
            Expiration = 1;
            Emmiter = "TEMPLATE";
            ValidIn = "https://localhost";
        }

        /// <summary>
        /// 
        /// </summary>
        public string Secret { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public int Expiration { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Emmiter { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string ValidIn { get; private set; }
    }
}
