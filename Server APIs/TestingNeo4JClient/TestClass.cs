using System;
namespace TestingNeo4JClient
{
    public class TestClass
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }

        public TestClass()
        {
        }

        public override string ToString()
        {
            return string.Format("[TestClass: Property1={0}, Property2={1}]", Property1, Property2);
        }
    }
}
