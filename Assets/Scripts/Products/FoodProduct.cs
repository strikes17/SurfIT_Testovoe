namespace Shop
{
    public class FoodProduct : AbstractProduct
    {
        protected float _weight;
        protected DateCompound _expirationDate;

        public FoodProduct(FoodProductObject foodProductObject) : base(foodProductObject)
        {
            _weight = foodProductObject.Weight;
            _expirationDate = foodProductObject.ExpirationDate;
        }

        public FoodProduct()
        {
            
        }
    }
}