using System;
using ServiceStack.Redis;

namespace RedisCommunicator
{
    public class RedisManager
    {
        static RedisManager _instance = new RedisManager();
        readonly RedisClient redis;

        public static RedisManager Instance { get => _instance; }

        private RedisManager()
        {
            string server = "77.81.229.55";
            int port = 6379;
            string pass = "nbpprojekat";

            redis = new RedisClient(server, port, pass);
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
