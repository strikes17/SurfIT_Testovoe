using UnityEngine;

namespace Shop
{
    public abstract class BaseProductObject : ScriptableObject
    {
        public int Cost;
        public int MaxCount;
        public CurrencyType CurrencyType;
        public string Title;
        [TextArea] public string Description;
    }
}