using System;
using System.Collections.Generic;
using System.Text;
using DataModel;

namespace Neo4jCommunicator
{
    public class CypherCodeGenerator
    {
        private static CypherCodeGenerator _instance = new CypherCodeGenerator();

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
        public string GenerateNewNodeCypherQuery(Node tObject)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("create(n: ");

            Type type = typeof(Node);
            builder.Append(type.Name + " {");

            var properties = type.GetProperties();

            //CREATE(n: Person { name: 'Andres', title: 'Developer' })

            foreach(var property in properties)
            {
                var value = property.GetValue(tObject);
                string cypherProperty = property.Name + ": '" + value.ToString() + "', ";
                builder.Append(cypherProperty);
            }

            builder = new StringBuilder(builder.ToString().TrimEnd().TrimEnd(','));
            builder.Append("})");

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
            builder.Append(relObj.SecondObject.GetType().Name);
            builder.AppendLine();
            builder.Append("WHERE a." + relObj.FirstObject.IdentificatorName);
            builder.Append(" = '" + relObj.FirstObject.IdentificatorValue.ToString());
            builder.Append("' AND b." + relObj.SecondObject.IdentificatorName);
            builder.Append(" = '" + relObj.SecondObject.IdentificatorValue.ToString() + "'");
            builder.Append("CREATE (a)-[r:" + relObj.GetType().Name + "]->(b)");

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
                string temp = "(n" + ++counter + ": " + param.GetType().Name;
                builder.Append(temp);
                if (counter != paramsList.Count)
                    builder.Append("-- > ");
            });

            counter = 0;
            builder.AppendLine();
            builder.Append("WHERE ");

            paramsList.ForEach((param) =>
            {
                builder.Append("n" + ++counter + "." + param.IdentificatorName + " = " + param.IdentificatorValue);
                if (counter != paramsList.Count)
                    builder.Append(" AND ");
            });

            counter = 0;
            builder.AppendLine();
            builder.Append("RETURN ");

            paramsList.ForEach((param) =>
            {
                builder.Append("n" + ++counter);
                if (counter != paramsList.Count)
                    builder.Append(", ");
            });

            return builder.ToString();
        }
    }
}
