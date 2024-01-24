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

        public GUIProductWidget GetProductWidget(string internalName)
        {
            return _guiManager.ShopWidget.GetProductWidget(internalName);
        }

        private void CreateShopTransaction(GUIProductWidget productWidget, CurrencyType currencyType)
        {
            var productObject = productWidget.ProductObject;
            if (_playerManager.IfPlayerHasItem(productObject.name))
            {
                return;
            }

            var playerResources = _playerManager.Resources;
            var productCost = productObject.Cost;
            int cost = 0, offer = 0;
            foreach (var currencyCompound in productCost)
            {
                if (currencyCompound.CurrencyType == currencyType)
                    cost = currencyCompound.Value;
            }

            offer = playerResources[currencyType];

            var shopTransaction = new ShopTransaction()
            {
                Offer = offer,
                Cost = cost,
                ProductObject = productObject,
                TimeToExpire = productObject.TimeToExpire
            };
            AbstractProduct product = null;
            if (currencyType == CurrencyType.Time)
            {
                product = shopTransaction.ProductObject.CreateProductInstance();
                productWidget.LockState = ProductLockState.UnlockedTimered;
                _playerManager.AddProductTimered(product, productObject.TimeToExpire);
                Debug.Log("Success Timered Product!");
            }
            else if (shopTransaction.Accomplish())
            {
                product = shopTransaction.ProductObject.CreateProductInstance();
                _playerManager.Resources[currencyType] -= shopTransaction.Cost;
                productWidget.LockState = ProductLockState.Unlocked;
                _playerManager.AddProduct(product);
                Debug.Log("Success!");
            }
            else
            {
                productWidget.LockState = ProductLockState.Locked;
                Debug.Log($"Failed! Not enough {currencyType.ToString()}");
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
                    var widget = _guiManager.ShopWidget.CreateProductWidgetInstance(baseProductObject);
                    widget.LockState = _playerManager.IfPlayerHasItem(baseProductObject.name)
                        ? ProductLockState.Unlocked
                        : ProductLockState.Locked;
                }
            }
        }
    }
}