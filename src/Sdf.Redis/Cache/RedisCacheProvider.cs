using Sdf.Fundamentals.Cache;
using Sdf.Fundamentals.Serializer;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sdf.Redis.Cache
{
    public class RedisCacheProvider : ICache
    {
        private readonly ISerializer _serializer;
        private readonly RedisCacheOption _redisCacheOption;
        public RedisCacheProvider(ISerializer serializer, RedisCacheOption redisCacheOption)
        {
            _serializer = serializer;
            _redisCacheOption = redisCacheOption;
        }
        private IDatabase Database
        {
            get
            {
                var connectionMultiplexer= StackExchangeRedisWrapper.GetConnectionMultiplexer(_redisCacheOption);

                return connectionMultiplexer.GetDatabase(_redisCacheOption.DbNumber);
            }
        }
        public async Task ClearAsync()
        {
            var connectionMultiplexer = StackExchangeRedisWrapper.GetConnectionMultiplexer(_redisCacheOption);
            string[] redisArr = _redisCacheOption.Host.Split(',');
            foreach (var item in redisArr)
            {
                var server = connectionMultiplexer.GetServer(item);
                await server.FlushDatabaseAsync(_redisCacheOption.DbNumber);
            }
        }

        public async Task<object> GetAsync(string key)
        {
            string json = await Database.StringGetAsync(key);
            if (string.IsNullOrEmpty(json))
                return null;

            return await _serializer.DeserializeAsync(json);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            
            string json = Database.StringGet(key);
            if (string.IsNullOrEmpty(json))
                return default;

            return await _serializer.DeserializeAsync<T>(json);
        }

        public async Task RemoveAsync(string key)
        {
            await Database.KeyDeleteAsync(key);
        }

        public async Task SetAsync(string key, object value)
        {
            TimeSpan? ts = _redisCacheOption.DefaultTimeSpan;
            await InternalSet(key, value, ts);
        }

        public async Task SetAsync(string key, object value, DateTime absoluteExpiration)
        {
            await InternalSet(key, value, absoluteExpiration - DateTime.Now);
        }

        public async Task SetAsync(string key, object value, TimeSpan slidingExpiration)
        {
            await InternalSet(key, value, slidingExpiration);
        }
        public async Task<bool> KeyExistsAsync(string key)
        {
            return await Database.KeyExistsAsync(key);
        }
        private async Task InternalSet(string key, object value, TimeSpan? slidingExpiration)
        {
            string json = await _serializer.SerializeAsync(value);
            await Database.StringSetAsync(key, json, slidingExpiration);
        }

      
    }
}
