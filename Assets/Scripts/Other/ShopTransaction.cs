using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class ShopTransaction
    {
        public BaseProductObject ProductObject;
        public int Offer, Cost;
        public CurrencyType CurrencyType;
        public float TimeToExpire;

        public bool Accomplish()
        {
            if (Offer >= Cost) return true;
            return false;
        }
    }
}