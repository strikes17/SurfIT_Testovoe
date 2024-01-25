using System.Linq;
using Shop.GUI;
using UnityEngine;

namespace Shop
{
    public class CheatsManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private ProductsManager _productsManager;
        [SerializeField] private GUIManager _guiManager;

        private void Start()
        {
            var resources = _playerManager.Resources;
            _guiManager.GoldCheatButton.Clicked.AddListener(() => { resources[CurrencyType.Gold] += 10; });

            _guiManager.GemsCheatButton.Clicked.AddListener(() => { resources[CurrencyType.Gem] += 1; });

            var timeScaleCheatButton = _guiManager.TimeScaleCheatButton;
            timeScaleCheatButton.Clicked.AddListener(() =>
            {
                var timeScale = Time.timeScale;
                var targetTimeScale = timeScale > 15 ? 1f : 16f;
                Time.timeScale = targetTimeScale;
                timeScaleCheatButton.Text = $"Timescale {Mathf.RoundToInt(targetTimeScale).ToString()}";
            });

            _guiManager.UnlockAllCheatButton.Clicked.AddListener(() =>
            {
                var allProductWidgets = _guiManager.ShopWidget.AllProductWidgets.ToList();
                foreach (var productWidget in allProductWidgets)
                {
                    _productsManager.CreateShopTransaction(productWidget, CurrencyType.Free);
                }
            });
        }
    }
}