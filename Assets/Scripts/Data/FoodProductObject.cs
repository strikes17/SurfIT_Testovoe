using System;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "Create New Food Product", fileName = "New Food Product")]
    public class FoodProductObject : BaseProductObject
    {
        [SerializeField] protected float _weight;
        [SerializeField] protected DateCompound _expirationDate;

        public float Weight => _weight;
        public DateCompound ExpirationDate => _expirationDate;
    }
}