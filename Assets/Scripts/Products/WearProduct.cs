using UnityEngine;

namespace Shop
{
    public class WearProduct : AbstractProduct
    {
        protected WearBodyTarget _bodyTarget;
        protected int _physicalProtectionValue;
        protected int _fireProtectionValue;
        protected int _weight;
        protected Color _color;
        
        public int Weight => _weight;
        public Color Color => _color;
        public int PhysicalProtectionValue => _physicalProtectionValue;
        public int FireProtectionValue => _fireProtectionValue;
        public WearBodyTarget BodyTarget => _bodyTarget;
    }
}