using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Sdf.Redis
{
    public class StackExchangeRedisWrapper
    {
        private readonly static object lockObject = new();

        private static readonly Dictionary<string, ConnectionMultiplexer> ConnectionMultiplexerDic = new();
        public static ConnectionMultiplexer GetConnectionMultiplexer(RedisConnectionOption redisConnection)
        {
            if (redisConnection == null)
            {
                throw new Exception("Redis没有正确配置");
            }
            if (!ConnectionMultiplexerDic.TryGetValue(redisConnection.Host, out ConnectionMultiplexer connectionMultiplexer))
            {
                lock (lockObject)
                {
                    if (!ConnectionMultiplexerDic.TryGetValue(redisConnection.Host, out connectionMultiplexer))
                    {
                        if (string.IsNullOrEmpty(redisConnection.Host))
                            throw new Exception("Redis没有正确配置");
                        var option = new ConfigurationOptions();
                        string[] redisArr = redisConnection.Host.Split(',');
                        foreach (var item in redisArr)
                        {
                            option.EndPoints.Add(item);
                        }
                        if (!string.IsNullOrEmpty(redisConnection.Password))
                        {
                            option.Password = redisConnection.Password;
                        }
                        connectionMultiplexer = ConnectionMultiplexer.Connect(option);
                        ConnectionMultiplexerDic.Add(redisConnection.Host, connectionMultiplexer);
                    }
                }
            }
            return connectionMultiplexer;

        }
    }
}
