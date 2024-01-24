using System;
using UnityEngine;

namespace Shop
{
    [Serializable]
    public class CurrencyCompound
    {
        [SerializeField] protected CurrencyType _currencyType;
        [SerializeField] protected int _value;

        public int Value => _value;
        public CurrencyType CurrencyType => _currencyType;
    }
}