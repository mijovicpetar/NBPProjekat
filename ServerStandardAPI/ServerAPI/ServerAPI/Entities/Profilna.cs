﻿using System;
namespace CombinedAPI.Entities
{
    public class Profilna : Relationship
    {
        private Node _firstObject;
        private Node _secondObject;

        public Profilna(Node first, Node second)
        {
            _firstObject = first;
            _secondObject = second;
        }

        public override Node FirstObject { get => _firstObject; set => _firstObject = value; }
        public override Node SecondObject { get => _secondObject; set => _secondObject = value; }
    }
}
