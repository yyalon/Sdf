using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Sdf.Core;
using Sdf.Fundamentals.Logs;
using Sdf.NLogLogger;
using System;

namespace Sdf.NLogLogger
{
    public static class SdfConfigManagerExtensions
    {
        public static SdfConfigManager UseNLog(this SdfConfigManager sdfConfig, string nlogConfigFile=null,NLogProviderOptions options =null) 
        {
            if (string.IsNullOrEmpty(nlogConfigFile))
            {
                nlogConfigFile = "nlog.config";
            }
            LogManager.LoadConfiguration(nlogConfigFile);
            sdfConfig.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, NLogLoggerProvider>(serviceProvider => CreateNLogLoggerProvider(options)));
            //sdfConfig.Register.RegisterGenericTransient(typeof(ILog<>), typeof(Log<>));
            sdfConfig.Register.RegisterTransient<ILog, Log>();
            return sdfConfig;
        }
        private static NLogLoggerProvider CreateNLogLoggerProvider(NLogProviderOptions options)
        {
            NLogLoggerProvider provider = new NLogLoggerProvider(options??new NLogProviderOptions(), null);
            return provider;
        }
        
    }
}
