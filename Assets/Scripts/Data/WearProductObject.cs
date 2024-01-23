using System;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "Create New Wear Product", fileName = "New Wear Product")]
    public class WearProductObject : BaseProductObject
    {
        [SerializeField] protected int _physicalProtectionValue;
        [SerializeField] protected int _fireProtectionValue;
        [SerializeField] protected int _weight;
        [SerializeField] protected Color _color;
        [SerializeField] protected WearBodyTarget _bodyTarget;
 
        public int Weight => _weight;
        public Color Color => _color;
        public int PhysicalProtectionValue => _physicalProtectionValue;
        public int FireProtectionValue => _fireProtectionValue;
        public WearBodyTarget BodyTarget => _bodyTarget;
    }
}