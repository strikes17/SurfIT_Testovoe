using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.GUI
{
    public class GUIProductWidget : GUIBaseWidget
    {
        public event GUIShopWidget.OnPurchaseEventDelegate PurchaseButtonClicked
        {
            add => _purchaseButtonClicked += value;
            remove => _purchaseButtonClicked -= value;
        }
        private event GUIShopWidget.OnPurchaseEventDelegate _purchaseButtonClicked;
        
        [SerializeField] private Transform _buyButtonsRoot;
        [SerializeField] private Image _iconImage;
        [SerializeField] private GUIBuyButtonWidget _buyButtonWidgetPrefab;

        private BaseProductObject _productObject;
        private List<GUIBuyButtonWidget> _widgets = new();

        public BaseProductObject ProductObject
        {
            get => _productObject;
            set
            {
                _productObject = value;
                _iconImage.sprite = _productObject.IconSprite;
                foreach (var currencyCompound in _productObject.Cost)
                {
                    var widget = Instantiate(_buyButtonWidgetPrefab, _buyButtonsRoot);
                    var currencyType = currencyCompound.CurrencyType;
                    widget.CurrencyType = currencyType;
                    widget.CostValue = currencyCompound.Value;
                    widget.ClickedEvent.AddListener(() =>
                    {
                        _purchaseButtonClicked?.Invoke(_productObject, currencyType);
                    });
                }
            }
        }
    }
}