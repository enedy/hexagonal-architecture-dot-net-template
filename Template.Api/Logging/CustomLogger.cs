namespace Template.Api.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="config"></param>
        public CustomLogger(string name, CustomLoggerProviderConfiguration config)
        {
            this.loggerName = name;
            this.loggerConfig = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            string mensagem = string.Format("{0}: {1} - {2}", logLevel.ToString(),
                eventId.Id, formatter(state, exception));
            SendMessageRepository(logLevel, mensagem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="mensagem"></param>
        private void SendMessageRepository(LogLevel logLevel, string mensagem)
        {
            if (logLevel == LogLevel.Error)
            {
                //var path = @"c:\log\";
                //var fileName = @"project-log.txt";
                //var completePath = path + fileName;

                //if (!Directory.Exists(path))
                //    Directory.CreateDirectory(path);

                //using (StreamWriter streamWriter = new StreamWriter(completePath, true))
                //{
                //    streamWriter.WriteLine(mensagem);
                //    streamWriter.Close();
                //}
            }
        }
    }
}
