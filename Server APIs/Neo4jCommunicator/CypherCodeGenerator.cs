using System;
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

        ///Generates cypher query for adding a new Node of
        ///Can be used for object of any class that extends Node class
        ///it will automatically recognize the class name
        ///and all properties and property values.
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

        ///Gets any object that extends Relationship class and generates
        ///cypher query for creating relation object in Neo4j database.
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
    }
}
