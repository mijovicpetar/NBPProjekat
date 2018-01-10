using System;
using System.Collections.Generic;
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

            //string query = CypherCodeGenerator.Instance.GenerateNewNodeCypherQuery<TestClass>(obj);

            //using(CommunicatorNeo4J comm = new CommunicatorNeo4J("bolt://localhost:7687", "neo4j", "nbpprojekat"))
            //{
            //    comm.ExecuteQuery(query);
            //}

            List<string> result = Neo4jManager.Instance.ExecuteMatchQuery<string>("MATCH (n:TestClass) RETURN n LIMIT 25");
        }
    }
}
