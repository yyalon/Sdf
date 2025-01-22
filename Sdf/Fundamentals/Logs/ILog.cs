using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Fundamentals.Logs
{
    public interface ILog
    {
        #region LogTrace
        /**
         * 记录一些对程序员调试问题有帮助的信息
         * 其中可能包含一些敏感信息, 所以应该避免在生产环境中启用Trace日志
         */
        string LogTrace(EventId eventId, Exception exception, string message, params object[] args);

        string LogTrace(EventId eventId, string message, params object[] args);

        string LogTrace(Exception exception, string message, params object[] args);

        string LogTrace(string message, params object[] args);
        #endregion

        #region LogDebug
        /**
         * 记录一些在开发和调试阶段有用的短时变量(Short-term usefulness),所以除非为了临时排除生产环境的故障
         * 开发人员应该尽量避免在生产环境中启用Debug日志
         */
        string LogDebug(EventId eventId, Exception exception, string message, params object[] args);

        string LogDebug(EventId eventId, string message, params object[] args);

        string LogDebug(Exception exception, string message, params object[] args);

        string LogDebug(string message, params object[] args);
        #endregion

        #region LogInformation
        /**
         * 记录应用程序的一些流程, 例如，记录当前api请求的url
         */
        string LogInformation(EventId eventId, Exception exception, string message, params object[] args);

        string LogInformation(EventId eventId, string message, params object[] args);

        string LogInformation(Exception exception, string message, params object[] args);

        string LogInformation(string message, params object[] args);
        #endregion

        #region LogWarning
        /**
         * 记录应用程序中发生的不正常或者未预期的事件信息。
         * 这些信息中可能包含错误消息或者错误产生的条件, 例如, 文件未找到
         */
        string LogWarning(EventId eventId, Exception exception, string message, params object[] args);

        string LogWarning(EventId eventId, string message, params object[] args);

        string LogWarning(Exception exception, string message, params object[] args);

        string LogWarning(string message, params object[] args);
        #endregion

        #region LogError
        /**
         * 记录应用程序中某个操作产生的错误和异常信息。
         */
        string LogError(string message, params object[] args);
        string LogError(Exception exception, string message, params object[] args);

        string LogError(EventId eventId, string message, params object[] args);

        string LogError(EventId eventId, Exception exception, string message, params object[] args);
        #endregion

        #region LogCritical
        /**
         * 记录一些需要立刻修复的问题。例如数据丢失，磁盘空间不足。
         */
        string LogCritical(EventId eventId, Exception exception, string message, params object[] args);

        string LogCritical(EventId eventId, string message, params object[] args);

        string LogCritical(Exception exception, string message, params object[] args);

        string LogCritical(string message, params object[] args);
        #endregion
       
    }
}
