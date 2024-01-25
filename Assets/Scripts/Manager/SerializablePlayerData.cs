using System;
using System.Collections.Generic;

namespace Shop.Serialization
{
    [Serializable]
    public class SerializablePlayerData
    {
        [Serializable]
        public class SerializableTimeredProducts
        {
            public SerializableTimeredProducts()
            {
            }

            public string productName;
            public float timeToExpire;
        }

        public SerializablePlayerData()
        {
        }

        public int gold, gems;
        public List<string> unlockedProducts  = new();
        public List<SerializableTimeredProducts> unlockedProductsTimered = new();
    }
}