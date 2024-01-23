using UnityEngine;

namespace Shop
{
    public class WearProduct : AbstractProduct
    {
        public enum WearBodyTarget
        {
            Head, Torso, Hand, Foot
        }
        protected int _size;
        protected Color _color;
    }
}