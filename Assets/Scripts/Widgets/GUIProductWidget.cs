using System;
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
        [SerializeField] private Image _iconImage, _lockImage;
        [SerializeField] private GUIBuyButtonWidget _buyButtonWidgetPrefab;
        [SerializeField] private GUIProductTimerWidget _timerWidget;

        private ProductLockState _productLockState;
        private List<GUIBuyButtonWidget> _buyButtons = new();
        private BaseProductObject _productObject;

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
                    _buyButtons.Add(widget);
                    var currencyType = currencyCompound.CurrencyType;
                    widget.CurrencyType = currencyType;
                    widget.CostValue = currencyCompound.Value;
                    widget.ClickedEvent.AddListener(() => { _purchaseButtonClicked?.Invoke(this, currencyType); });
                }
            }
        }

        public ProductLockState LockState
        {
            get => _productLockState;
            set
            {
                _productLockState = value;
                _timerWidget.Hide();
                if (_productLockState == ProductLockState.Locked)
                {
                    foreach (var buttonWidget in _buyButtons)
                    {
                        buttonWidget.Show();
                    }

                    _lockImage.gameObject.SetActive(true);
                }
                else if (_productLockState == ProductLockState.Unlocked)
                {
                    foreach (var buttonWidget in _buyButtons)
                    {
                        buttonWidget.Hide();
                    }

                    _lockImage.gameObject.SetActive(false);
                }
                else if (_productLockState == ProductLockState.UnlockedTimered)
                {
                    foreach (var buttonWidget in _buyButtons)
                    {
                        buttonWidget.Hide();
                    }

                    _lockImage.gameObject.SetActive(false);
                    _timerWidget.Show();
                }
            }
        }

        public GUIProductTimerWidget TimerWidget => _timerWidget;
    }
}