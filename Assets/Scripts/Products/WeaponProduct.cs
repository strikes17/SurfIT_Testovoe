namespace Shop
{
    public class WeaponProduct : AbstractProduct
    {
        protected int _baseDamage;
        protected float _weight;

        public int BaseDamage => _baseDamage;
        public float Weight => _weight;

        public WeaponProduct(WeaponProductObject weaponProductObject)
        {
            _baseDamage = weaponProductObject.BaseDamage;
            _weight = weaponProductObject.Weight;
        }

        public WeaponProduct()
        {
        }
    }
}