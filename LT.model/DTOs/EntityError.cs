using Microsoft.Extensions.Logging;
using System.Threading;

namespace LT.model
{
    public class EntityErrorDto : EntityBaseDto
    {
        public string LogLevel {get; set;} = string.Empty;
        public int ThreadId {get; set;}
        public int EventId {get; set;}
        public string? EventName {get; set;} = string.Empty;
        public string? ExceptionMessage {get; set; } = string.Empty;
        public string? ExceptionStackTrace {get; set; } = string.Empty;
        public string? ExceptionSource {get; set; } = string.Empty;

        public EntityErrorDto(string logLevel, int threadId, int eventId, string? eventName, string? exceptionMessage, string? exceptionStackTrace, string? exceptionSource)
        {
            LogLevel = logLevel;
            ThreadId = threadId;
            EventId = eventId;
            EventName = eventName;
            ExceptionMessage = exceptionMessage;
            ExceptionStackTrace = exceptionStackTrace;
            ExceptionSource = exceptionSource;
        }
    }
}