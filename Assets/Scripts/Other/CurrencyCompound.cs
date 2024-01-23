using System;
using UnityEngine;

namespace Shop
{
    [Serializable]
    public class CurrencyCompound
    {
        [SerializeField] protected int _value;

        public int Value => _value;
    }
}