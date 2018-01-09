using System;
using ServiceStack.Redis;

namespace RedisCommunicator
{
    public class CommunicatorRedis
    {
        readonly RedisClient redis;

        public CommunicatorRedis(string server)
        {
            redis = new RedisClient(server);
        }

        public void SetValue(string key, object value)
        {
            redis.Add(key, value);
        }

        public object GetValue(string key)
        {
            return redis.Get(key);
        }
    }
}
