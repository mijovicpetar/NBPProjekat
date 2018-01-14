using System;
using System.Collections.Generic;
using System.Text;

namespace CombinedAPI.Entities
{
    public interface IIdentificator
    {
        string IdentificatorName { get; set; }
        string IdentificatorValue { get; set; }
        bool UseInWhereClause { get; set; }
    }

    public abstract class Node : IIdentificator
    {
        public abstract string IdentificatorName { get; set; }
        public abstract string IdentificatorValue { get; set; }
        public abstract bool UseInWhereClause { get; set; }
    }
}
