using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Redis
{
    public class StackExchangeRedisWrapper
    {
        private readonly static object lockObject = new object();

        private static Dictionary<string, ConnectionMultiplexer> ConnectionMultiplexerDic = new Dictionary<string, ConnectionMultiplexer>();
        public static ConnectionMultiplexer GetConnectionMultiplexer(RedisConnectionOption redisConnection)
        {
            if (redisConnection == null)
            {
                throw new Exception("Redis没有正确配置");
            }
            ConnectionMultiplexer connectionMultiplexer = null;
            if (!ConnectionMultiplexerDic.TryGetValue(redisConnection.Host, out connectionMultiplexer))
            {
                lock (lockObject)
                {
                    if (!ConnectionMultiplexerDic.TryGetValue(redisConnection.Host, out connectionMultiplexer))
                    {
                        if (String.IsNullOrEmpty(redisConnection.Host))
                            throw new Exception("Redis没有正确配置");
                        ConfigurationOptions option = new ConfigurationOptions();
                        string[] redisArr = redisConnection.Host.Split(',');
                        foreach (var item in redisArr)
                        {
                            option.EndPoints.Add(item);
                        }
                        if (!String.IsNullOrEmpty(redisConnection.Password))
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
