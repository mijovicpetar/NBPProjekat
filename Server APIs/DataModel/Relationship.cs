using System;
namespace DataModel
{
    public interface IRelation
    {
        Node FirstObject { get; set; }
        Node SecondObject { get; set; }
    }

    public abstract class Relationship : IRelation
    {
        public abstract Node FirstObject { get; set; }
        public abstract Node SecondObject { get; set; }
    }
}
