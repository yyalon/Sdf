using Microsoft.Extensions.DependencyInjection;
using Sdf.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider AddSdf(this IServiceCollection services, Action<SdfConfigManager> action)
        {
            return Bootstrapper.Instance.Initialize(services, action);
        }
    }
}
