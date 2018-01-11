using System;
using DataModel;

namespace TestingNeo4JClient
{
    public class TestClass :Node
    {
        string _identificatorName;
        object _identificatorValue;
        bool _useInWhereClaues;

        public override string IdentificatorName { get => _identificatorName; set => _identificatorName = value; }
        public override object IdentificatorValue { get => _identificatorValue; set => _identificatorValue = value; }
        public override bool UseInWhereClause { get => _useInWhereClaues; set => _useInWhereClaues = value; }

        public TestClass()
        {
        }

        public override string ToString()
        {
            return string.Format("[TestClass: IdentificatorName={0}, IdentificatorValue={1}, UseInWhereClause={2}]", IdentificatorName, IdentificatorValue, UseInWhereClause);
        }
    }
}
