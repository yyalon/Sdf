using Microsoft.Extensions.DependencyInjection;
using Sdf.Core;
using System;

namespace Sdf.Extensions
{
    public class SdfServiceContainer
    {
        public IServiceCollection Services { get; set; }
        public IServiceProvider ServiceProvider { get; private set; }
        public void Initialize(Action<SdfConfigManager> action)
        {
            ServiceProvider = Services.AddSdf(action);
        }
    }
}
