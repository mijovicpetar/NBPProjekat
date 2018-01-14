using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack.Redis;

namespace CombinedAPI.redis
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

        public void SetJsonValue(string key, object complexObj)
        {
            redis.Add(key, Newtonsoft.Json.JsonConvert.SerializeObject(complexObj));
        }

        public void SSetValue(string setName, string value)
        {
            redis.AddItemToSet(setName, value);
        }

        public bool SSetIsInSet(string setName, string value)
        {
            var result = redis.SIsMember(setName, Encoding.ASCII.GetBytes(value));
            return Convert.ToBoolean(result);
        }

        public void SRemoveValue(string setName, string value)
        {
            redis.RemoveItemFromSet(setName, value);
        }

        public List<string> GetAllFromSet(string setName)
        {
            List<string> resultList = new List<string>();
            var result = redis.GetAllItemsFromSet(setName);

            foreach(var item in result)
            {
                resultList.Add(item);
            }

            return resultList;
        }

        public void DeleteEverything()
        {
            redis.FlushAll();
        }
    }
}
