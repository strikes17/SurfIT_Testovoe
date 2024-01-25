using System;
using System.Collections.Generic;
using System.Linq;
using Shop.GUI;
using UnityEngine;

namespace Shop
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GUIManager _guiManager;
        protected Dictionary<CurrencyType, int> _resources = new();
        protected Dictionary<string, AbstractProduct> _products = new();
        protected Dictionary<string, float> _timeredProductsTimerValues = new();
        protected List<string> _timeredProductsNames = new();

        public Dictionary<CurrencyType, int> Resources => _resources;

        private void Start()
        {
            _resources.Add(CurrencyType.Gold, 100);
            _resources.Add(CurrencyType.Gem, 5);
            _resources.Add(CurrencyType.Rub, 0);
            _resources.Add(CurrencyType.Usd, 0);
            _resources.Add(CurrencyType.Time, 0);
            _resources.Add(CurrencyType.Free, 0);
        }

        private void Update()
        {
            var keys = _timeredProductsNames;
            for (int i = 0; i < keys.Count; i++)
            {
                var key = _timeredProductsNames[i];
                if (!_timeredProductsTimerValues.ContainsKey(key)) continue;

                AbstractProduct product = null;
                if (!_products.TryGetValue(key, out product)) continue;
                product.TimeLeft -= Time.deltaTime;
                var timerWidget = _guiManager.ShopWidget.GetProductWidget(key).TimerWidget;
                timerWidget.Value = (int)product.TimeLeft;
                if (product.TimeLeft <= 0)
                    RemoveProductByInternalName(key);
            }

            _guiManager.GoldResourceWidget.Value = _resources[CurrencyType.Gold];
            _guiManager.GemsResourceWidget.Value = _resources[CurrencyType.Gem];
        }

        public void AddProduct(AbstractProduct product)
        {
            _products.Add(product.InternalName, product);
            _timeredProductsNames.Add(product.InternalName);
        }

        public void AddProductTimered(AbstractProduct product, float timeInSeconds)
        {
            AddProduct(product);
            _timeredProductsTimerValues.Add(product.InternalName, timeInSeconds);
        }

        public void RemoveProductByInternalName(string internalName)
        {
            _timeredProductsNames.Remove(internalName);
            _products.Remove(internalName);
            if (_timeredProductsTimerValues.ContainsKey(internalName))
            {
                _timeredProductsTimerValues.Remove(internalName);
                _guiManager.ShopWidget.GetProductWidget(internalName).LockState = ProductLockState.Locked;
            }
        }

        public bool IfPlayerHasItem(string name)
        {
            return _products.ContainsKey(name);
        }
    }
}