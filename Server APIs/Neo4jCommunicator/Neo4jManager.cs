using System;
using DataModel;

namespace Neo4jCommunicator
{
    public class Neo4jManager
    {
        static Neo4jManager _instance = new Neo4jManager();
        CommunicatorNeo4J client;

        private Neo4jManager()
        {
            string server = "bolt://localhost:7687";
            string user = "neo4j";
            string password = "nbpprojekat";

            client = new CommunicatorNeo4J(server, user, password);
        }

        public void GenerateNewNode<T>(T tObject)
        {
            string query = CypherCodeGenerator.Instance.GenerateNewNodeCypherQuery<T>(tObject);
            client.ExecuteQuery(query);
        }

        public void GenerateNewRelation(IRelationPrimar relObj)
        {
            string query = CypherCodeGenerator.Instance.GenerateNewRelationCypherQuery(relObj);
            client.ExecuteQuery(query);
        }

        public static Neo4jManager Instance { get => _instance; }
    }
}
