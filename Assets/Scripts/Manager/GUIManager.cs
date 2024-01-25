using UnityEngine;

namespace Shop.GUI
{
    public class GUIManager : MonoBehaviour
    {
        [SerializeField] private GUIShopWidget _shopWidget;
        [SerializeField] private GUIResourceWidget _goldResourceWidget, _gemsResourceWidget;

        [SerializeField]
        private GUICheatButton _goldCheatButton, _gemsCheatButton, _timeScaleCheatButton, _unlockAllCheatButton;

        public GUIShopWidget ShopWidget => _shopWidget;

        public GUIResourceWidget GoldResourceWidget => _goldResourceWidget;

        public GUIResourceWidget GemsResourceWidget => _gemsResourceWidget;

        public GUICheatButton GoldCheatButton => _goldCheatButton;

        public GUICheatButton GemsCheatButton => _gemsCheatButton;

        public GUICheatButton TimeScaleCheatButton => _timeScaleCheatButton;

        public GUICheatButton UnlockAllCheatButton => _unlockAllCheatButton;
    }
}