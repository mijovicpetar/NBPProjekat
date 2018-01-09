using System;
using Neo4jCommunicator;

namespace TestingNeo4JClient
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            using(CommunicatorNeo4J comm = new CommunicatorNeo4J("bolt://localhost:7687", "neo4j", "nbpprojekat"))
            {
                object result = comm.Query("MATCH(n: Movie) RETURN n LIMIT 25");
                Console.WriteLine(result);
            }
        }
    }
}
