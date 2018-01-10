using System;
using System.Linq;
using Neo4j.Driver.V1;

namespace Neo4jCommunicator
{
    public class CommunicatorNeo4J : IDisposable
    {
        private readonly IDriver _driver;

        public CommunicatorNeo4J(string uri, string user, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
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

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}
