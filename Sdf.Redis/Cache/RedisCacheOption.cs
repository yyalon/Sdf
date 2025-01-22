using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Redis.Cache
{
    public class RedisCacheOption: RedisConnectionOption
    {
        public string CacheKeyPrefix { get; set; }
        public int DbNumber { get; set; }
        public int? ExpirationMinute { get; set; }
        public TimeSpan? DefaultTimeSpan
        {
            get
            {
                if (ExpirationMinute.HasValue)
                    return new TimeSpan(0, ExpirationMinute.Value, 0);
                else
                    return null;
            }
        }
    }
}
