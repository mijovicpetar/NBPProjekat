using System;
namespace DataModel
{
    public interface IIdentificator
    {
        string IdentificatorName { get; set; }
        object IdentificatorValue { get; set; }
    }

    public abstract class Node : IIdentificator
    {
        public abstract string IdentificatorName { get; set; }
        public abstract object IdentificatorValue { get; set; }
    }
}
