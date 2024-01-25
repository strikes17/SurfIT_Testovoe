using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.GUI
{
    public class GUIShopWidget : GUIBaseWidget
    {
        public delegate void OnPurchaseEventDelegate(GUIProductWidget baseProductObject, CurrencyType currencyType);

        public event OnPurchaseEventDelegate PurchaseButtonClicked
        {
            add => _purchaseButtonClicked += value;
            remove => _purchaseButtonClicked -= value;
        }

        private event OnPurchaseEventDelegate _purchaseButtonClicked;

        [SerializeField] private GUIProductWidget _productWidgetPrefab;
        [SerializeField] private Transform _productsRootTransform;
        private Dictionary<string, GUIProductWidget> _productWidgets = new();
        public IEnumerable<GUIProductWidget> AllProductWidgets => _productWidgets.Values;

        public GUIProductWidget GetProductWidget(string internalName)
        {
            _productWidgets.TryGetValue(internalName, out GUIProductWidget widget);
            return widget;
        }

        public GUIProductWidget CreateProductWidgetInstance(BaseProductObject baseProductObject)
        {
            var widget = Instantiate(_productWidgetPrefab, _productsRootTransform);
            _productWidgets.Add(baseProductObject.name, widget);

            widget.ProductObject = baseProductObject;
            widget.PurchaseButtonClicked += _purchaseButtonClicked;
            return widget;
        }
    }
}