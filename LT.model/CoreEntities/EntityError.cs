using Microsoft.Extensions.Logging;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LT.model
{
    public class EntityError : EntityBase
    {
        public string LogLevel {get; set;} = string.Empty;
        public int ThreadId {get; set;}
        public int EventId {get; set;}
        public string? EventName {get; set;} = string.Empty;
        public string? ExceptionMessage {get; set; } = string.Empty;
        public string? ExceptionStackTrace {get; set; } = string.Empty;
        public string? ExceptionSource {get; set; } = string.Empty;

        private EntityError(string logLevel, int threadId, int eventId, string? eventName, string? exceptionMessage, string? exceptionStackTrace, string? exceptionSource)
        {
            LogLevel = logLevel;
            ThreadId = threadId;
            EventId = eventId;
            EventName = eventName;
            ExceptionMessage = exceptionMessage;
            ExceptionStackTrace = exceptionStackTrace;
            ExceptionSource = exceptionSource;
        }

        public static EntityError Create(string logLevel, int threadId, int eventId, string? eventName, string? exceptionMessage, string? exceptionStackTrace, string? exceptionSource)
        {
            return new EntityError(logLevel, threadId, eventId, eventName, exceptionMessage, exceptionStackTrace, exceptionSource);
        }

    }
}