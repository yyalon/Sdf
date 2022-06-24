using Sdf.Core;
using Sdf.Fundamentals.Cache;
using Sdf.Redis.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Redis
{
    public static class SdfConfigManagerExtensions
    {
        public static SdfConfigManager UseRedisCache(this SdfConfigManager sdfConfig, Func<IResolver, RedisCacheOption> action)
        {
            if (action == null)
                throw new Exception("必须配置redis");
            sdfConfig.Register.RegisterSingleton(resolver =>
            {
                return action(resolver);
            });
            sdfConfig.Register.RegisterSingleton<RedisConnectionOption>(resolver =>
            {
                return resolver.Resolve<RedisCacheOption>();
            });
             
            sdfConfig.Register.RegisterSingleton<ICache, RedisCacheProvider>();
            return sdfConfig;
        }
    }
}
