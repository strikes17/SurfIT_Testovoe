using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.GUI
{
    public class GUIShopWidget : GUIBaseWidget
    {
        public delegate void OnPurchaseEventDelegate(BaseProductObject baseProductObject, CurrencyType currencyType);

        public event OnPurchaseEventDelegate PurchaseButtonClicked
        {
            add => _purchaseButtonClicked += value;
            remove => _purchaseButtonClicked -= value;
        }

        private event OnPurchaseEventDelegate _purchaseButtonClicked;

        [SerializeField] private GUIProductWidget _productWidgetPrefab;
        [SerializeField] private Transform _productsRootTransform;
        private List<GUIProductWidget> _widgets = new();

        public void CreateProductWidgetInstance(BaseProductObject baseProductObject)
        {
            var widget = Instantiate(_productWidgetPrefab, _productsRootTransform);
            _widgets.Add(widget);

            widget.ProductObject = baseProductObject;
            widget.PurchaseButtonClicked += _purchaseButtonClicked;
        }
    }
}