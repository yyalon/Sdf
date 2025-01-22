using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Fundamentals.Logs
{
    public interface ILoggerFactory
    {
        ILog CreateLogger(string name);
    }
}
