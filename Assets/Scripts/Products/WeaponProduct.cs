namespace Shop
{
    public class WeaponProduct : AbstractProduct
    {
        protected int _baseDamage;
        protected float _weight;

        public int BaseDamage => _baseDamage;
        public float Weight => _weight;
    }
}