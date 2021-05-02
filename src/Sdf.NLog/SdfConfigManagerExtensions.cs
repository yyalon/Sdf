using NLog;
using NLog.Targets;
using Sdf.Core;
using Sdf.Fundamentals.Logs;

namespace Sdf.NLogLogger
{
    public static class SdfConfigManagerExtensions
    {
        public static SdfConfigManager UseNLog(this SdfConfigManager sdfConfig, string nlogConfigFile=null) 
        {
            if (string.IsNullOrEmpty(nlogConfigFile))
            {
                nlogConfigFile = "nlog.config";
            }
            LogManager.LoadConfiguration(nlogConfigFile);
           
            sdfConfig.Register.RegisterTransient<ILog, Log>();
            sdfConfig.Register.RegisterSingleton<ILoggerFactory, NLoggerFactory>();
            return sdfConfig;
        }
        //private static NLogLoggerProvider CreateNLogLoggerProvider(NLogProviderOptions options)
        //{
        //    NLogLoggerProvider provider = new NLogLoggerProvider(options??new NLogProviderOptions(), null);
        //    return provider;
        //}
        
    }
}
