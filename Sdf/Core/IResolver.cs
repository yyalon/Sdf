using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Core
{
    public interface IResolver : IDisposable
    {
        T Resolve<T>(string name = null);
        object Resolve(Type serviceType, string name = null);
    }
}
