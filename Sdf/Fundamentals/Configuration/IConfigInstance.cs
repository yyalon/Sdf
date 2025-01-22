using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Fundamentals.Configuration
{
    public interface IConfig<T> : IConfig where T : class
    {
        T Instance { get; }
    }
}
