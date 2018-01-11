﻿using System;
using System.Collections.Generic;
using DataModel;

namespace Neo4jCommunicator
{
    /// <summary>
    /// Neo4j manager class.
    /// This is singleton class which is accessed by using Instance propery.
    /// </summary>
    public class Neo4jManager
    {
        static Neo4jManager _instance = new Neo4jManager();
        CommunicatorNeo4J client;

        public static Neo4jManager Instance { get => _instance; }

        private Neo4jManager()
        {
            string server = "bolt://localhost:7687";
            string uri = "http://localhost:7474/db/data";
            string user = "neo4j";
            string password = "nbpprojekat";

            try
            {
                client = new CommunicatorNeo4J(server, uri, user, password);
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        /// <summary>
        /// Generates the new node in connected neo4j database.
        /// </summary>
        /// <param name="tObject">Object of class that extends Node class.</param>
        public void GenerateNewNode(object tObject)
        {
            string query = CypherCodeGenerator.Instance.GenerateNewNodeCypherQuery(tObject);
            client.ExecuteQuery(query);
        }

        /// <summary>
        /// Generates the new relation in connected neo4j database.
        /// </summary>
        /// <param name="relObj">Object of class that extends Relationship class.</param>
        public void GenerateNewRelation(Relationship relObj)
        {
            string query = CypherCodeGenerator.Instance.GenerateNewRelationCypherQuery(relObj);
            client.ExecuteQuery(query);
        }

        /// <summary>
        /// Executes the match query. And returns a list of wanted type.
        /// </summary>
        /// <returns>The match query.</returns>
        /// <param name="conditions">Conditions.</param>
        /// <typeparam name="T">List of Node object, it uses their properties
        /// IdentificatorName and IdentificatorValue based on which query is
        /// generated and executed. It returns all node of the target path in
        /// order like this: (a)-->(b)-->(c). If you want to use the node in
        /// where clause set the UseInWhereClause property to true.
        /// This should be used only if all Nodes are the same type.
        /// </typeparam>
        public List<T> ExecuteMatchQuery<T> (List<Node> conditions)
        {
            string query = CypherCodeGenerator.Instance.GenerateMatchCypherQuery(conditions);
            return client.ExecuteQuery<T>(query);
        }

        /// <summary>
        /// Executes the match query. This should be used for custom queries
        /// which are not supported with other methods.
        /// </summary>
        /// <returns>The match query.</returns>
        /// <param name="query">String query.</param>
        /// <typeparam name="T">List of Node object, it uses their properties
        /// IdentificatorName and IdentificatorValue based on which query is
        /// generated and executed.</typeparam>
        public List<T> ExecuteMatchQuery<T> (string query)
        {
            return client.ExecuteQuery<T>(query);
        }

        /// <summary>
        /// Executes the query.This should be used for custom queries
        /// which are not supported with other methods and when no return value
        /// is expected.
        /// </summary>
        /// <param name="query">Query.</param>
        public void ExecuteQuery(string query)
        {
            client.ExecuteQuery(query);
        }
    }
}
