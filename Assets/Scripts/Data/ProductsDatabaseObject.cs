using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(menuName = "Create New Products Database", fileName = "New Products Database")]
    public class ProductsDatabaseObject : ScriptableObject
    {
        [SerializeField] private List<BaseProductObject> _products;

        public List<BaseProductObject> Products => _products;
    }
}