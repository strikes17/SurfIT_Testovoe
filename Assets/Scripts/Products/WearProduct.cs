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

        public WearProduct(WearProductObject wearProductObject)
        {
            _bodyTarget = wearProductObject.BodyTarget;
            _physicalProtectionValue = wearProductObject.PhysicalProtectionValue;
            _fireProtectionValue = wearProductObject.FireProtectionValue;
            _weight = wearProductObject.Weight;
            _color = wearProductObject.Color;
        }

        public WearProduct()
        {
        }
    }
}