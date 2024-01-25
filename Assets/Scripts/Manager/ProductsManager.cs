using System;
using System.Collections.Generic;
using System.Linq;
using Shop.GUI;
using UnityEngine;

namespace Shop
{
    public class ProductsManager : MonoBehaviour
    {
        public delegate void OnProductTransactionEventDelegate(AbstractProduct product);

        public event OnProductTransactionEventDelegate TransactionSuccess
        {
            add => _transactionSuccess += value;
            remove => _transactionSuccess -= value;
        }

        private event OnProductTransactionEventDelegate _transactionSuccess;

        protected virtual void OnTransactionSuccess(AbstractProduct product)
        {
            _transactionSuccess?.Invoke(product);
        }

        [SerializeField] private List<ProductsDatabaseObject> _productsDatabaseObjects;
        [SerializeField] private GUIManager _guiManager;
        [SerializeField] private PlayerManager _playerManager;

        private void Awake()
        {
            LoadProductsInGui();
        }

        public AbstractProduct GetProductInstanceFromDataObject(string internalName)
        {
            var b = _productsDatabaseObjects.FirstOrDefault(x => x.Products.FirstOrDefault()?.name == internalName);
            if (b == null) return null;
            var productObject = b.Products.FirstOrDefault(x => x.name == internalName);
            if (productObject == null) return null;
            var instance = productObject.CreateProductInstance();
            return instance;
        }

        public void CreateShopTransaction(GUIProductWidget productWidget, CurrencyType currencyType)
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
                OnTransactionSuccess(product);
                Debug.Log($"Unlocked Product: {product.InternalName} for {productObject.TimeToExpire}seconds!");
            }
            else if (currencyType == CurrencyType.Free)
            {
                product = shopTransaction.ProductObject.CreateProductInstance();
                productWidget.LockState = ProductLockState.Unlocked;
                _playerManager.AddProduct(product);
                OnTransactionSuccess(product);
                Debug.Log($"Free Unlock Of Product! {product.InternalName}");
            }
            else if (shopTransaction.Accomplish())
            {
                product = shopTransaction.ProductObject.CreateProductInstance();
                _playerManager.Resources[currencyType] -= shopTransaction.Cost;
                productWidget.LockState = ProductLockState.Unlocked;
                _playerManager.AddProduct(product);
                OnTransactionSuccess(product);
                Debug.Log(
                    $"Unlocked Product! {product.InternalName} for cost of: {shopTransaction.Cost} {currencyType}");
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