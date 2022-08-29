using Microsoft.Extensions.Logging;

namespace Template.Api.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomLoggerProviderConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        /// <summary>
        /// 
        /// </summary>
        public int EventId { get; set; } = 0;
    }
}
