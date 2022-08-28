using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Template.Api.Logging
{
    public class CustomLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;
        public CustomLogger(string name, CustomLoggerProviderConfiguration config)
        {
            this.loggerName = name;
            loggerConfig = config;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            string mensagem = string.Format("{0}: {1} - {2}", logLevel.ToString(),
                eventId.Id, formatter(state, exception));
            SendMessageRepository(logLevel, mensagem);
        }
        private void SendMessageRepository(LogLevel logLevel, string mensagem)
        {
            if (logLevel == LogLevel.Error)
            {
                //var path = @"c:\log\";
                //var fileName = @"my-first-project-log.txt";
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
