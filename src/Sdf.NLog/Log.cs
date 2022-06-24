using Microsoft.Extensions.Logging;
using NLog;
using Sdf.Fundamentals.Logs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.NLogLogger
{
    public class Log : ILog
    {

        private readonly Logger _logger;
        public Log()
        {
            _logger = LogManager.GetLogger("default");
        }
        public Log(Logger logger)
        {
            _logger = logger;
        }
        private string GetNewEventId()
        {
            return Guid.NewGuid().ToString("N");
        }
       
        private LogEventInfo CreateLogEventInfo(EventId eventGroupId,string eventId, NLog.LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            LogEventInfo logEventInfo = new LogEventInfo();
            logEventInfo.Exception = exception;
            logEventInfo.Message = args!=null && args.Length>0? string.Format(message, args): message;
            logEventInfo.Level = logLevel;
            logEventInfo.Properties["EventGroupId"] = string.IsNullOrEmpty(eventGroupId.Name) ? eventGroupId.Id.ToString() : eventGroupId.Name;
            logEventInfo.Properties["EventId"] = eventId;
            return logEventInfo;
        }
        #region LogTrace
        public string LogTrace(EventId eventGroupId, Exception exception, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(eventGroupId, eventId, NLog.LogLevel.Trace, exception, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogTrace(EventId eventGroupId, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(eventGroupId, eventId, NLog.LogLevel.Trace, null, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogTrace(Exception exception, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(new EventId(), eventId, NLog.LogLevel.Trace, exception, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogTrace(string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(new EventId(), eventId, NLog.LogLevel.Trace, null, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }
        #endregion

        #region LogDebug
        public string LogDebug(EventId eventGroupId, Exception exception, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(eventGroupId, eventId, NLog.LogLevel.Debug, exception, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogDebug(EventId eventGroupId, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(eventGroupId, eventId, NLog.LogLevel.Debug, null, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogDebug(Exception exception, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(new EventId(), eventId, NLog.LogLevel.Debug, exception, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogDebug(string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(new EventId(), eventId, NLog.LogLevel.Debug, null, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }
        #endregion

        #region LogInformation
        public string LogInformation(EventId eventGroupId, Exception exception, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(eventGroupId, eventId, NLog.LogLevel.Info, exception, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogInformation(EventId eventGroupId, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(eventGroupId, eventId, NLog.LogLevel.Info, null, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogInformation(Exception exception, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(new EventId(), eventId, NLog.LogLevel.Info, exception, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogInformation(string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(new EventId(), eventId, NLog.LogLevel.Info, null, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }
        #endregion

        #region LogWarning
        public string LogWarning(EventId eventGroupId, Exception exception, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(eventGroupId, eventId, NLog.LogLevel.Warn, exception, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogWarning(EventId eventGroupId, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(eventGroupId, eventId, NLog.LogLevel.Warn, null, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogWarning(Exception exception, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(new EventId(), eventId, NLog.LogLevel.Warn, exception, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogWarning(string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(new EventId(), eventId, NLog.LogLevel.Warn, null, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }
        #endregion

        

        #region LogError
        public string LogError(EventId eventGroupId, Exception exception, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(eventGroupId, eventId, NLog.LogLevel.Error, exception, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogError(EventId eventGroupId, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(eventGroupId, eventId, NLog.LogLevel.Error, null, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogError(Exception exception, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(new EventId(), eventId, NLog.LogLevel.Error, exception, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            //_logger.Error(exception, message);
            return eventId;
        }

        public string LogError(string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(new EventId(), eventId, NLog.LogLevel.Error, null, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }
        #endregion

        #region LogCritical
        public string LogCritical(EventId eventGroupId, Exception exception, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(eventGroupId, eventId, NLog.LogLevel.Fatal, exception, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogCritical(EventId eventGroupId, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(eventGroupId, eventId, NLog.LogLevel.Fatal, null, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogCritical(Exception exception, string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(new EventId(), eventId, NLog.LogLevel.Fatal, exception, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }

        public string LogCritical(string message, params object[] args)
        {
            string eventId = GetNewEventId();
            LogEventInfo logEventInfo = CreateLogEventInfo(new EventId(), eventId, NLog.LogLevel.Fatal, null, message, args);
            _logger.Log(typeof(Log), logEventInfo);
            return eventId;
        }
        #endregion
    }
}
