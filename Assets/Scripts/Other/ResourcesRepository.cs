using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class ResourcesRepository<T> where T : UnityEngine.Object
    {
        public static ResourcesRepository<T> Instance => _instance ?? new ResourcesRepository<T>();
        private static ResourcesRepository<T> _instance;

        public T Get(string name)
        {
            if (_cached.TryGetValue(name, out var resource))
                return resource;
            resource = Resources.Load<T>(name);
            _cached.TryAdd(name, resource);
            return resource;
        }

        private readonly Dictionary<string, T> _cached = new();
    }
}