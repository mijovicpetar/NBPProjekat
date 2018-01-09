using System;
using RedisCommunicator;

namespace TestingRedisClient
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            try
            {
                CommunicatorRedis redis = new CommunicatorRedis("localhost");
                redis.SetValue("kljuc", "vrednost");
                var result = redis.GetValue("kljuc");
                if (result != null)
                    Console.WriteLine("Proso je.");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
