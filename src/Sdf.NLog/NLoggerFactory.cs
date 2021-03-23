using NLog;
using Sdf.Fundamentals.Logs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.NLogLogger
{
    public class NLoggerFactory: ILoggerFactory
    {
        public ILog CreateLogger(string name)
        {
            var logger = LogManager.GetLogger(name);
            if (logger == null)
                return null;
            return new Log(logger);
        }
    }
}
