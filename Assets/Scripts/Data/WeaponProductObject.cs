using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "Create New Weapon Product", fileName = "New Weapon Product")]
    public class WeaponProductObject : BaseProductObject
    {
        public int BaseDamage;
        public float Weight;
    }
}