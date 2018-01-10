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

        //Generates cypher query for adding a new Node of
        //T type. Can be used for object of any class,
        //it will automatically recognize the class name
        //and all properties and property values.
        public string GenerateNewNodeCypherQuery<T>(T tObject)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("create(n: ");

            Type type = typeof(T);
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

        //Gets any object that implements IRelation interface and generates
        //cypher query for creating relation object in Neo4j database.
        public string GenerateNewRelationCypherQuery(IRelationPrimar relObj)
        {
            Type type = relObj.GetType();

            var firstObj = type.GetProperty("FirstObject");
            var secondObj = type.GetProperty("SecondObject");

            if (firstObj == null || secondObj == null)
            {
                throw new Exception("Passed relationship object doesn't implement IRelation interface.");
            }

            var foIdentName = firstObj.GetType().GetProperty("IdentificatorName");
            var foIdentValue = firstObj.GetType().GetProperty("IdentificatorValue");
            var soIdentName = secondObj.GetType().GetProperty("IdentificatorName");
            var soIdentValue = secondObj.GetType().GetProperty("IdentificatorValue");

            if(foIdentName == null || foIdentValue == null
               || soIdentName == null || soIdentValue == null)
            {
                throw new Exception("Passed relationship object objects doesn't implement IIdentificator interface.");
            }

            string firstObjTypeName = firstObj.GetType().Name;
            string secondObjTypeName = secondObj.GetType().Name;

            //MATCH(a:Person),(b:Person)
            //WHERE a.name = 'Node A' AND b.name = 'Node B'
            //CREATE (a)-[r:RELTYPE]->(b)

            StringBuilder builder = new StringBuilder();
            builder.Append("MATCH(a:");
            builder.Append(firstObjTypeName);
            builder.Append("),(b:");
            builder.Append(secondObjTypeName);
            builder.AppendLine();
            builder.Append("WHERE a." + foIdentName.GetValue(foIdentName).ToString());
            builder.Append(" = '" + foIdentValue.GetValue(foIdentValue).ToString());
            builder.Append("' AND b." + soIdentName.GetValue(soIdentName).ToString());
            builder.Append(" = '" + soIdentValue.GetValue(soIdentValue).ToString() + "'");
            builder.Append("CREATE (a)-[r:" + type.Name + "]->(b)");

            return builder.ToString();
        }
    }
}
