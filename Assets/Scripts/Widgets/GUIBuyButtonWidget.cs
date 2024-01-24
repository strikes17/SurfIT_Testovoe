using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.GUI
{
    public class GUIBuyButtonWidget : GUIBaseWidget
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private Button _button;

        public Button.ButtonClickedEvent ClickedEvent => _button.onClick;
        private CurrencyType _currencyType;

        public int CostValue
        {
            set => _costText.text = value.ToString();
        }

        public CurrencyType CurrencyType
        {
            get => _currencyType;
            set
            {
                _currencyType = value;
                _iconImage.sprite =
                    ResourcesRepository<Sprite>.Instance.Get(GlobalVariables.CURRENCY_ICONS_PATH[_currencyType]);
            }
        }
    }
}