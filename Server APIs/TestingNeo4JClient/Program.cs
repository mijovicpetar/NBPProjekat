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
            obj.IdentificatorName = "IdentificatorValue";
            obj.IdentificatorValue = "prop2";

            //List<TestClass> result = Neo4jManager.Instance.ExecuteMatchQuery<TestClass>("MATCH (n:TestClass) RETURN n LIMIT 25");
            //Console.WriteLine(result[0].ToString());

            Neo4jManager.Instance.GenerateNewNode(obj);
        }
    }
}
