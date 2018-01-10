using System;
namespace DataModel
{
    public interface IRelationPrimar { }

    public interface IRelation<T1, T2> : IEquatable<IRelation<T1, T2>>, IRelationPrimar
    {
        T1 FirstObject { get; set; }
        T2 SecondObject { get; set; }
    }

    public abstract class Relationship<T1, T2> : IRelation<T1, T2>
    {
        public abstract T1 FirstObject { get; set; }
        public abstract T2 SecondObject { get; set; }

        public abstract bool Equals(IRelation<T1, T2> other);
    }
}
