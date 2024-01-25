using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Shop.GUI;
using Shop.Serialization;
using UnityEngine;
using System.Text;

namespace Shop
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private ProductsManager _productsManager;
        [SerializeField] private GUIManager _guiManager;
        protected Dictionary<CurrencyType, int> _resources = new();
        protected Dictionary<string, AbstractProduct> _products = new();
        protected Dictionary<string, float> _timeredProductsTimerValues = new();
        protected List<string> _timeredProductsNames = new();

        public Dictionary<CurrencyType, int> Resources => _resources;

        private void OnApplicationQuit()
        {
            SaveData();
            Debug.Log("saved");
        }

        public void SaveData()
        {
            SerializablePlayerData playerData = new SerializablePlayerData();
            playerData.gold = _resources[CurrencyType.Gold];
            playerData.gems = _resources[CurrencyType.Gem];
            foreach (var (key, value) in _products)
            {
                playerData.unlockedProducts.Add(key);
            }

            foreach (var productName in _timeredProductsNames)
            {
                playerData.unlockedProductsTimered.Add(new SerializablePlayerData.SerializableTimeredProducts()
                {
                    productName = productName,
                    timeToExpire = _timeredProductsTimerValues[productName]
                });
            }

            var path = $"{Application.persistentDataPath}/Saves";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var filePath = path + "/save1.json";
            var json = JsonUtility.ToJson(playerData, true);
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(json);
            string encodedText = Convert.ToBase64String(bytesToEncode);
            File.WriteAllText(filePath, encodedText);
        }

        public void LoadData()
        {
            var path = $"{Application.persistentDataPath}/Saves";
            var filePath = path + "/save1.json";
            if (!File.Exists(filePath))
            {
                return;
            }

            var text = File.ReadAllText(filePath);
            byte[] decodedBytes = Convert.FromBase64String(text);
            string decodedText = Encoding.UTF8.GetString(decodedBytes);
            var playerData = JsonUtility.FromJson<SerializablePlayerData>(decodedText);
            var unlockedProducts = playerData.unlockedProducts;
            foreach (var internalName in unlockedProducts)
            {
                var instance = _productsManager.GetProductInstanceFromDataObject(internalName);
                _products.Add(internalName, instance);
                var widget = _guiManager.ShopWidget.GetProductWidget(internalName);
                if (widget != null)
                    widget.LockState = ProductLockState.Unlocked;
            }

            var unlockedProductsTimered = playerData.unlockedProductsTimered;
            foreach (var timeredProduct in unlockedProductsTimered)
            {
                var internalName = timeredProduct.productName;
                var time = timeredProduct.timeToExpire;
                _timeredProductsNames.Add(internalName);
                _timeredProductsTimerValues.Add(internalName, time);
                _guiManager.ShopWidget.GetProductWidget(internalName).LockState = ProductLockState.UnlockedTimered;
            }

            _resources[CurrencyType.Gold] = playerData.gold;
            _resources[CurrencyType.Gem] = playerData.gems;
        }

        private void Start()
        {
            _resources.Add(CurrencyType.Gold, 100);
            _resources.Add(CurrencyType.Gem, 5);
            _resources.Add(CurrencyType.Rub, 0);
            _resources.Add(CurrencyType.Usd, 0);
            _resources.Add(CurrencyType.Time, 0);
            _resources.Add(CurrencyType.Free, 0);
            LoadData();
            _productsManager.TransactionSuccess += product => { SaveData(); };
        }

        private void Update()
        {
            var keys = _timeredProductsNames;
            for (int i = 0; i < keys.Count; i++)
            {
                var key = _timeredProductsNames[i];
                if (!_timeredProductsTimerValues.ContainsKey(key)) continue;
                _timeredProductsTimerValues[key] -= Time.deltaTime;
                var timerWidget = _guiManager.ShopWidget.GetProductWidget(key).TimerWidget;
                timerWidget.Value = (int)_timeredProductsTimerValues[key];
                if (_timeredProductsTimerValues[key] <= 0)
                    RemoveProductByInternalName(key);
            }

            _guiManager.GoldResourceWidget.Value = _resources[CurrencyType.Gold];
            _guiManager.GemsResourceWidget.Value = _resources[CurrencyType.Gem];
        }

        public void AddProduct(AbstractProduct product)
        {
            _products.Add(product.InternalName, product);
        }

        public void AddProductTimered(AbstractProduct product, float timeInSeconds)
        {
            AddProduct(product);
            _timeredProductsNames.Add(product.InternalName);
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