using System.Collections.Generic;

namespace Shop
{
    public class ShopTransaction
    {
        public BaseProductObject ProductObject;
        public Dictionary<CurrencyType, int> Offer;
        public Dictionary<CurrencyType, int> Cost;
        public float TimeToExpire;

        public int IsPossible
        {
            get
            {
                foreach (var (key, value) in Cost)
                {
                    bool enough = Offer[key] >= value;
                    if (!enough) return (int)key;
                }

                return 0;
            }
        }

        public void Accomplish()
        {
            foreach (var (key, value) in Offer)
            {
                Offer[key] -= Cost[key];
            }
        }
    }
}