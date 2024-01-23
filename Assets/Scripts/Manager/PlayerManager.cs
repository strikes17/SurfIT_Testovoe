using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class PlayerManager : MonoBehaviour
    {
        protected Dictionary<CurrencyType, int> _resources;
        protected Dictionary<string, AbstractProduct> _products = new();

        public Dictionary<CurrencyType, int> Resources => _resources;

        public void AddProduct(AbstractProduct product)
        {
            _products.Add(product.InternalName, product);
        }

        public void RemoveProductByInternalName(string name)
        {
            _products.Remove(name);
        }
    }
}