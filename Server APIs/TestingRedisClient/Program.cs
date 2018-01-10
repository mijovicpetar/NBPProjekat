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
                CommunicatorRedis redis = new CommunicatorRedis(
                    "77.81.229.55", 
                    6379, 
                    "nbpprojekat");
                
                redis.SetValue("kljuc", "vrednost");
                byte[] result = (byte[])redis.GetValue("kljuc");
                if (result != null)
                    Console.WriteLine(Encoding.UTF8.GetString(result));
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}
