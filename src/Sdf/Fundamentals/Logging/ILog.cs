using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Fundamentals.Logging
{
    public interface ILog
    {
        /// <summary>
        /// 写入Debug日志
        /// </summary>
        /// <param name="msg"></param>
        void Debug(string msg);
        /// <summary>
        /// 写入普通消息日志
        /// </summary>
        /// <param name="msg"></param>
        void Info(string msg);
        /// <summary>
        /// 写入警告级别的日志
        /// </summary>
        /// <param name="msg"></param>
        void Warn(string msg);
        /// <summary>
        /// 写入危险等级的日志
        /// </summary>
        /// <param name="msg"></param>
        void Error(string msg);
        void Fatal(string msg);
        /// <summary>
        /// 写入操作成功的日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        void Debug(string msg, Exception ex);
        void Info(string msg, Exception ex);
        void Warn(string msg, Exception ex);
        void Error(string msg, Exception ex);
        void Fatal(string msg, Exception ex);
        LogLevel LogLevel { get; }
    }
}
