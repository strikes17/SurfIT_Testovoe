using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shop
{
    public abstract class BaseProductObject : ScriptableObject
    {
        [SerializeField] protected Sprite _iconSprite;
        [SerializeField] protected List<CurrencyCompound> _cost;
        [SerializeField] protected int _maxCount;
        [SerializeField] protected string _title;
        [SerializeField] [TextArea] protected string _description;

        public List<CurrencyCompound> Cost => _cost;
        public int MaxCount => _maxCount;
        public string Title => _title;
        public string Description => _description;
        public Sprite IconSprite => _iconSprite;

        public abstract AbstractProduct CreateInstance();
    }
}