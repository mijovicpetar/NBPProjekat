using System;
using System.Collections.Generic;
using System.Text;

namespace CombinedAPI.Entities
{
    public class OwnerSlike : Relationship
    {
        private Node _firstObject;
        private Node _secondObject;

        public OwnerSlike(Node first, Node second)
        {
            _firstObject = first;
            _secondObject = second;
        }

        public override Node FirstObject { get => _firstObject; set => _firstObject = value; }
        public override Node SecondObject { get => _secondObject; set => _secondObject = value; }
    }
}
