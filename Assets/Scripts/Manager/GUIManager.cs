using UnityEngine;

namespace Shop.GUI
{
    public class GUIManager : MonoBehaviour
    {
        [SerializeField] private GUIShopWidget _shopWidget;

        public GUIShopWidget ShopWidget => _shopWidget;
    }
}