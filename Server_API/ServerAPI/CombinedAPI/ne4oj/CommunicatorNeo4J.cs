using System;
using System.Collections.Generic;
using System.Linq;
using Neo4j.Driver.V1;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace CombinedAPI.neo4j
{
    public class CommunicatorNeo4J : IDisposable
    {
        private readonly IDriver _driver;
        private GraphClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CombinedAPI.neo4j.CommunicatorNeo4J"/> class.
        /// </summary>
        /// <param name="server">Server.</param>
        /// <param name="uri">URI.</param>
        /// <param name="user">Username.</param>
        /// <param name="password">Password.</param>
        public CommunicatorNeo4J(string server, string uri, string user, string password)
        {
            _driver = GraphDatabase.Driver(server, AuthTokens.Basic(user, password));
            _client = new GraphClient(new Uri(uri), user, password);
            _client.Connect();
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="query">Query.</param>
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

        /// <summary>
        /// Executes the query t.
        /// </summary>
        /// <returns>The query t.</returns>
        /// <param name="query">Query.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public List<T> ExecuteQueryT<T>(string query)
        {
            var eQuery = new Neo4jClient.Cypher.CypherQuery(query,
                                                            new Dictionary<string, object>(),
                                                            CypherResultMode.Set);

            List<T> result = ((IRawGraphClient)_client).ExecuteGetCypherResults<T>(eQuery).ToList();

            return result;
        }

        /// <summary>
        /// Releases all resource used by the <see cref="T:CombinedAPI.neo4j.CommunicatorNeo4J"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="T:CombinedAPI.neo4j.CommunicatorNeo4J"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="T:CombinedAPI.neo4j.CommunicatorNeo4J"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:CombinedAPI.neo4j.CommunicatorNeo4J"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:CombinedAPI.neo4j.CommunicatorNeo4J"/> was occupying.</remarks>
        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}
