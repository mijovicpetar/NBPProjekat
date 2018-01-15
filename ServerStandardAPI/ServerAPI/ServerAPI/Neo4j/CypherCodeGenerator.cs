using System;
using System.Collections.Generic;
using System.Text;
using CombinedAPI.Entities;

namespace CombinedAPI.neo4j
{
    public enum ResultNodeSide
    {
        ResultNodeLeftSide,
        ResultNodeRightSide
    }

    public class CypherCodeGenerator
    {
        private static CypherCodeGenerator _instance = new CypherCodeGenerator();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CombinedAPI.neo4j.CypherCodeGenerator"/> class.
        /// </summary>
        private CypherCodeGenerator()
        {
        }

        public static CypherCodeGenerator Instance { get => _instance; set => _instance = value; }

        /// <summary>
        /// Generates cypher query for adding a new Node of
        /// Can be used for object of any class that extends Node class
        /// it will automatically recognize the class name
        /// and all properties and property values.
        /// </summary>
        /// <returns>The new node cypher query.</returns>
        /// <param name="tObject">T object.</param>
        public string GenerateNewNodeCypherQuery(object tObject)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("create(n: ");

            Type type = tObject.GetType();
            builder.Append(type.Name + " {");

            var properties = type.GetProperties();

            //CREATE(n: Person { name: 'Andres', title: 'Developer' })

            foreach (var property in properties)
            {
                if (ValidAtribute((property.Name)))
                {
                    var value = property.GetValue(tObject);
                    string cypherProperty = property.Name + ": '" + value.ToString() + "', ";
                    builder.Append(cypherProperty);
                }
            }

            builder = new StringBuilder(builder.ToString().TrimEnd().TrimEnd(','));
            builder.Append("})");

            return builder.ToString();
        }

        /// <summary>
        /// Generates the get node query.
        /// </summary>
        /// <returns>The get node query.</returns>
        /// <param name="tObject">T object.</param>
        public string GenerateGetNodeQuery(object tObject)
        {
            Node node = tObject as Node;
            StringBuilder builder = new StringBuilder();
            builder.Append("MATCH(n: "+ tObject.GetType().Name + " { ");
            builder.Append(node.IdentificatorName+": '"+node.IdentificatorValue + "'");
            builder.Append(" }) RETURN n");

            return builder.ToString();
        }

        /// <summary>
        /// Generates the edit node cypher query.
        /// </summary>
        /// <returns>The edit node cypher query.</returns>
        /// <param name="tObject">T object.</param>
        public string GenerateEditNodeCypherQuery(object tObject)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("MATCH(n: { ");

            Type type = tObject.GetType();
            var properties = type.GetProperties();

            // MATCH(n { name: 'Andres' })
            // SET n.position = 'Developer', n.surname = 'Taylor'

            builder.Append((tObject as Node).IdentificatorName + ": '"
                           + (tObject as Node).IdentificatorValue + "' }) ");
            builder.Append("SET ");

            foreach (var property in properties)
            {
                if (ValidAtribute(property.Name))
                {
                    var value = property.GetValue(tObject);
                    string cypherProperty = "n." + property.Name + " = '" + value.ToString() + "', ";
                    builder.Append(cypherProperty);
                }
            }

            builder = new StringBuilder(builder.ToString().TrimEnd().TrimEnd(','));
            builder.Append("})");

            return builder.ToString();
        }

        /// <summary>
        /// Generates the delete node query, and all its relations.
        /// </summary>
        /// <returns>The delete node query.</returns>
        /// <param name="tObject">T object.</param>
        public string GenerateDeleteNodeQuery(object tObject)
        {
            // MATCH(n { name: 'Andres' })
            // DETACH DELETE n

            StringBuilder builder = new StringBuilder();
            builder.Append("MATCH(n { ");
            builder.Append((tObject as Node).IdentificatorName + ": '"
                           + (tObject as Node).IdentificatorValue + "' }) ");
            builder.Append("DETACH DELETE n");

            return builder.ToString();
        }

        /// <summary>
        /// Generates the delete relationship query.
        /// </summary>
        /// <returns>The delete relationship query.</returns>
        /// <param name="tObject">T object.</param>
        public string GenerateDeleteRelationshipQuery(object tObject)
        {
            // MATCH(:Artist { Name: "Strapping Young Lad"})
            // -[r: RELEASED] - 
            // (: Album { Name: "Heavy as a Really Heavy Thing"}) 
            // DELETE r

            Node firstNode = (tObject as Relationship).FirstObject;
            Node secondNode = (tObject as Relationship).SecondObject;

            StringBuilder builder = new StringBuilder();
            builder.Append("MATCH(" + firstNode.GetType().Name + " { ");
            builder.Append(firstNode.IdentificatorName + ": '"
                           + firstNode.IdentificatorValue + "' }) ");
            builder.Append("-[r: " + tObject.GetType().Name + "] - ");
            builder.Append("(: " + secondNode.GetType().Name + " { ");
            builder.Append(firstNode.IdentificatorName + ": '"
                           + firstNode.IdentificatorValue + "' }) ");

            return builder.ToString();
        }

        /// <summary>
        /// Gets any object that extends Relationship class and generates
        /// cypher query for creating relation object in Neo4j database.
        /// </summary>
        /// <returns>The new relation cypher query.</returns>
        /// <param name="relObj">Rel object.</param>
        public string GenerateNewRelationCypherQuery(Relationship relObj)
        {
            //MATCH(a:Person),(b:Person)
            //WHERE a.name = 'Node A' AND b.name = 'Node B'
            //CREATE (a)-[r:RELTYPE]->(b)

            StringBuilder builder = new StringBuilder();
            builder.Append("MATCH(a:");
            builder.Append(relObj.FirstObject.GetType().Name);
            builder.Append("),(b:");
            builder.Append(relObj.SecondObject.GetType().Name + ")");
            builder.Append(" WHERE a." + relObj.FirstObject.IdentificatorName);
            builder.Append(" = '" + relObj.FirstObject.IdentificatorValue.ToString());
            builder.Append("' AND b." + relObj.SecondObject.IdentificatorName);
            builder.Append(" = '" + relObj.SecondObject.IdentificatorValue.ToString() + "'");
            builder.Append(" CREATE (a)-[r:" + relObj.GetType().Name + "]->(b)");

            return builder.ToString();
        }

        /// <summary>
        /// Gets any object that extends Relationship class and generates
        /// cypher query for geting any side Node object
        /// from relation in Neo4j database. Side of the Node is determined by every
        /// nodes propery UseInWhereClause. Both nodes can be used for query.
        /// </summary>
        /// <param name="relObj"></param>
        /// <param name="resultNodeSide"></param>
        /// <returns></returns>
        public string GenerateGetNodeFromRelationCypherQuery(Relationship relObj, ResultNodeSide resultNodeSide)
        {
            // MATCH (a: Slika) -[r: Tag]->(b: Profil { KorisnickoIme: 'aasd'})
            // RETURN s

            StringBuilder builder = new StringBuilder();
            builder.Append("MATCH(a: " + relObj.FirstObject.GetType().Name);

            if (relObj.FirstObject.UseInWhereClause)
            {
                builder.Append(" { " + relObj.FirstObject.IdentificatorName + ": '");
                builder.Append(relObj.FirstObject.IdentificatorValue + "'}");
            }

            builder.Append(")");
            builder.Append("-[r: " + relObj.GetType().Name + "]->");
            builder.Append("(b: " + relObj.SecondObject.GetType().Name);

            if (relObj.SecondObject.UseInWhereClause)
            {
                builder.Append(" { " + relObj.SecondObject.IdentificatorName + ": '");
                builder.Append(relObj.SecondObject.IdentificatorValue + "'}");
            }

            builder.Append(")");

            if (resultNodeSide == ResultNodeSide.ResultNodeLeftSide)
                builder.Append(" RETURN a");
            else
                builder.Append(" RETURN b");

            return builder.ToString();
        }

        /// <summary>
        /// Generates the match cypher query from list of Nodes.
        /// It use every node for the path and those who have true
        /// for UseInWhereClause for where clause.
        /// </summary>
        /// <returns>The match cypher query.</returns>
        /// <param name="paramsList">Parameters list.</param>
        public string GenerateMatchCypherQuery(List<Node> paramsList)
        {
            //MATCH(node1: Label1)-- > (node2: Label2)
            //WHERE node1.propertyA = 'value'
            //RETURN node1, node2

            StringBuilder builder = new StringBuilder();
            builder.Append("MATCH");
            int counter = 0;

            paramsList.ForEach((param) => 
            {
                string temp = "(n" + ++counter + ": " + param.GetType().Name + ")";
                builder.Append(temp);
                if (counter != paramsList.Count)
                    builder.Append("-- > ");
            });

            counter = 0;
            builder.Append(" WHERE ");

            paramsList.ForEach((param) =>
            {
                builder.Append("n" + ++counter + "." + param.IdentificatorName + " = '" + param.IdentificatorValue + "'");
                if (counter != paramsList.Count)
                    builder.Append(" AND ");
            });

            counter = 0;
            builder.Append(" RETURN ");

            paramsList.ForEach((param) =>
            {
                builder.Append("n" + ++counter);
                if (counter != paramsList.Count)
                    builder.Append(", ");
            });

            return builder.ToString();
        }

        /// <summary>
        /// Valids the atribute.
        /// </summary>
        /// <returns><c>true</c>, if atribute was valided, <c>false</c> otherwise.</returns>
        /// <param name="atrName">Atr name.</param>
        private bool ValidAtribute(string atrName)
        {
            if (atrName != "IdentificatorName"
                && atrName != "IdentificatorValue"
                && atrName != "UseInWhereClause")
                return true;
            else
                return false;
        }
    }
}
