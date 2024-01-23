using System;
using System.Collections.Generic;
using Shop.GUI;
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

    public class ShopTransaction
    {
        public BaseProductObject ProductObject;
        public Dictionary<CurrencyType, int> Offer;
        public Dictionary<CurrencyType, int> Cost;
        public float TimeToExpire;

        public int Success
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
    }

    public class ProductsManager : MonoBehaviour
    {
        [SerializeField] private List<ProductsDatabaseObject> _productsDatabaseObjects;
        [SerializeField] private GUIManager _guiManager;
        [SerializeField] private PlayerManager _playerManager;

        private void Start()
        {
            LoadProductsInGui();
        }

        private void TryToMakeADeal(BaseProductObject productObject, CurrencyType currencyType)
        {
            ShopTransaction shopTransaction = new ShopTransaction()
            {
                Cost = new Dictionary<CurrencyType, int>(),
                Offer = new Dictionary<CurrencyType, int>(),
                ProductObject = productObject,
                TimeToExpire = 100f
            };
            var transactionSuccess = shopTransaction.Success;
            if (transactionSuccess == 0)
            {
                var product = shopTransaction.ProductObject.CreateInstance();
                _playerManager.AddProduct(product);
            }
        }

        private void LoadProductsInGui()
        {
            _guiManager.ShopWidget.PurchaseButtonClicked += TryToMakeADeal;
            foreach (var databaseObject in _productsDatabaseObjects)
            {
                var products = databaseObject.Products;
                foreach (var baseProductObject in products)
                {
                    _guiManager.ShopWidget.CreateProductWidgetInstance(baseProductObject);
                }
            }
        }
    }
}