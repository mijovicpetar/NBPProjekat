using System;
using System.Collections.Generic;
using System.Linq;
using Neo4j.Driver.V1;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace Neo4jCommunicator
{
    public class CommunicatorNeo4J : IDisposable
    {
        private readonly IDriver _driver;
        private GraphClient _client;

        public CommunicatorNeo4J(string server, string uri, string user, string password)
        {
            _driver = GraphDatabase.Driver(server, AuthTokens.Basic(user, password));
            _client = new GraphClient(new Uri(uri), user, password);
            _client.Connect();
        }

        public void ExecuteQuery(string query)
        {
            using (var session = _driver.Session())
            {
                session.WriteTransaction(tx =>
                {
                    var result = tx.Run(query);
                });
            }
        }

        public List<T> ExecuteQuery<T>(string query)
        {
            var eQuery = new Neo4jClient.Cypher.CypherQuery(query,
                                                            new Dictionary<string, object>(),
                                                            CypherResultMode.Set);

            List<T> result = ((IRawGraphClient)_client).ExecuteGetCypherResults<T>(eQuery).ToList();

            return result;
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}
