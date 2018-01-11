using System;
using System.Text;
using RedisCommunicator;

namespace TestingRedisClient
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            try
            {
                //RedisManager redis = new RedisManager(
                //    "77.81.229.55", 
                //    6379, 
                //    "nbpprojekat");
                
                //redis.SetValue("kljuc", "vrednost");
                //byte[] result = (byte[])redis.GetValue("kljuc");
                //if (result != null)
                    //Console.WriteLine(Encoding.UTF8.GetString(result));
                RedisManager.Instance.SetValue("proba1", "proba1");
                byte[] result = (byte[])RedisManager.Instance.GetValue("proba1");
                Console.WriteLine(Encoding.UTF8.GetString(result));
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
