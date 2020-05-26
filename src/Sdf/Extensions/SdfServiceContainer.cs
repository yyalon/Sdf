using Microsoft.Extensions.DependencyInjection;
using Sdf.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Extensions
{
    public class SdfServiceContainer
    {
        public IServiceCollection Services { get; set; }
        public IServiceProvider serviceProvider { get; private set; }
        public void Initialize(Action<SdfConfigManager> action)
        {
            serviceProvider = Services.AddSdf(action);
        }
    }
}
