using Sdf.Fundamentals.Cache;
using Sdf.Fundamentals.Serializer;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

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
        public void Clear()
        {
            var connectionMultiplexer = StackExchangeRedisWrapper.GetConnectionMultiplexer(_redisCacheOption);
            string[] redisArr = _redisCacheOption.Host.Split(',');
            foreach (var item in redisArr)
            {
                var server = connectionMultiplexer.GetServer(item);
                server.FlushDatabase(_redisCacheOption.DbNumber);
            }
            
        }

        public object Get(string key)
        {
            string json = Database.StringGet(key);
            if (string.IsNullOrEmpty(json))
                return null;
            return _serializer.Deserialize(json);
        }

        public T Get<T>(string key)
        {
            
            string json = Database.StringGet(key);
            if (string.IsNullOrEmpty(json))
                return default(T);
            return _serializer.Deserialize<T>(json);
        }

        public void Remove(string key)
        {
            Database.KeyDelete(key);
        }

        public void Set(string key, object value)
        {
            TimeSpan? ts = _redisCacheOption.DefaultTimeSpan;
            _Set(key, value, ts);
        }

        public void Set(string key, object value, DateTime absoluteExpiration)
        {
            _Set(key, value, absoluteExpiration - DateTime.Now);
        }

        public void Set(string key, object value, TimeSpan slidingExpiration)
        {
            _Set(key, value, slidingExpiration);
        }
        public bool KeyExists(string key)
        {
            return Database.KeyExists(key);
        }
        private void _Set(string key, object value, TimeSpan? slidingExpiration)
        {
            string json = _serializer.Serialize(value);
            Database.StringSet(key, json, slidingExpiration);
        }
       
    }
}
