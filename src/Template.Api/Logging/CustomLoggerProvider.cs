using System.Collections.Concurrent;

namespace Template.Api.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomLoggerProvider : ILoggerProvider
    {
        readonly CustomLoggerProviderConfiguration loggerConfig;
        readonly ConcurrentDictionary<string, CustomLogger> loggers =
            new ConcurrentDictionary<string, CustomLogger>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public CustomLoggerProvider(CustomLoggerProviderConfiguration config)
        {
            loggerConfig = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string category)
        {
            return loggers.GetOrAdd(category, name => new CustomLogger(name, loggerConfig));
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose() { }
    }
}
