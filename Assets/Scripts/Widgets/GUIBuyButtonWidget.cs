using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.GUI
{
    public class GUIBuyButtonWidget : GUIBaseWidget
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Text _costText;
        [SerializeField] private Button _button;

        public Button.ButtonClickedEvent ClickedEvent => _button.onClick;
        private CurrencyType _currencyType;

        public int CostValue
        {
            set => _costText.text = _currencyType == CurrencyType.Free ? "FREE" : value.ToString();
        }

        public CurrencyType CurrencyType
        {
            get => _currencyType;
            set
            {
                _currencyType = value;
                Sprite sprite = null;
                _iconImage.gameObject.SetActive(true);
                if (GlobalVariables.CURRENCY_ICONS_PATH.TryGetValue(_currencyType, out string s))
                {
                    sprite = ResourcesRepository<Sprite>.Instance.Get(s);
                }

                _iconImage.sprite = sprite;
                if (_currencyType == CurrencyType.Free)
                {
                    _iconImage.gameObject.SetActive(false);
                }
            }
        }
    }
}