using System;
using System.Collections.Generic;
using Shop.GUI;
using UnityEngine;

namespace Shop
{
    public class ProductsManager : MonoBehaviour
    {
        [SerializeField] private List<ProductsDatabaseObject> _productsDatabaseObjects;
        [SerializeField] private GUIManager _guiManager;
        [SerializeField] private PlayerManager _playerManager;

        private void Start()
        {
            LoadProductsInGui();
        }

        private void CreateShopTransaction(BaseProductObject productObject, CurrencyType currencyType)
        {
            ShopTransaction shopTransaction = new ShopTransaction()
            {
                Cost = new Dictionary<CurrencyType, int>(),
                Offer = new Dictionary<CurrencyType, int>(),
                ProductObject = productObject,
                TimeToExpire = productObject.TimeToExpire
            };
            var productCost = productObject.Cost;
            foreach (var currencyCompound in productCost)
            {
                shopTransaction.Cost.Add(currencyCompound.CurrencyType, currencyCompound.Value);
            }

            var playerResources = _playerManager.Resources;
            shopTransaction.Offer = playerResources;

            var transactionCode = shopTransaction.IsPossible;
            if (transactionCode == 0)
            {
                var product = shopTransaction.ProductObject.CreateProductInstance();
                _playerManager.AddProduct(product);
                shopTransaction.Accomplish();
                Debug.Log("Success!");
            }
            else
            {
                Debug.Log($"Failed! Not enough {(CurrencyType)transactionCode}");
            }
        }

        private void LoadProductsInGui()
        {
            _guiManager.ShopWidget.PurchaseButtonClicked += CreateShopTransaction;
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