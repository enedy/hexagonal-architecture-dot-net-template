using System;

namespace Template.Shared.Core.DomainObjects
{
    public class DomainException : Exception
    {
        public DomainException(string userMessage, string systemMessage, string requestPayload)
        {
            UserMessage = userMessage;
            SystemMessage = systemMessage;
            RequestPayload = requestPayload;
        }

        public string UserMessage { get; private set; }
        public string SystemMessage { get; private set; }
        public string RequestPayload { get; private set; }
    }
}
