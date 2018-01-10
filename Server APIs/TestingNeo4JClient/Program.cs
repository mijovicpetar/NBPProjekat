using System;
using Neo4jCommunicator;

namespace TestingNeo4JClient
{
    class MainClass
    {

        public static void Main(string[] args)
        {
            TestClass obj = new TestClass();
            obj.Property1 = "prop1";
            obj.Property2 = "prop2";

            string query = CypherCodeGenerator.Instance.GenerateNewNodeCypherQuery<TestClass>(obj);

            using(CommunicatorNeo4J comm = new CommunicatorNeo4J("bolt://localhost:7687", "neo4j", "nbpprojekat"))
            {
                comm.CreateQuery(query);
            }
        }
    }
}
