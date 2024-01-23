using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "Create New Weapon Product", fileName = "New Weapon Product")]
    public class WeaponProductObject : BaseProductObject
    {
        [SerializeField] protected int _baseDamage;
        [SerializeField] protected float _weight;

        public int BaseDamage => _baseDamage;
        public float Weight => _weight;

        public override AbstractProduct CreateInstance()
        {
            return new WeaponProduct();
        }
    }
}