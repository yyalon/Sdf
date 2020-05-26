using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Fundamentals.Configuration
{
    public interface IConfig
    {
        string GetValue(string key);

        bool TryGetValue(string key, out string value);
    }
}
