using System;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Assets.Scripts.Services
{
    public class ObjectPool
    {
        private Dictionary<Type, Dictionary<MonoBehaviour, Queue<MonoBehaviour>>> _pool = new Dictionary<Type, Dictionary<MonoBehaviour, Queue<MonoBehaviour>>>();

        public T Get<T>(T original) where T: MonoBehaviour
        {
            T result = null;
            Type type = original.GetType();

            if (!_pool.ContainsKey(type) || _pool[type].ContainsKey(original) || _pool[type][original].Count == 0)
            {
                result = GameObject.Instantiate(original);
            }
            else
            {
                result = _pool[type][original].Dequeue() as T;
            }

            return result;
        }
    }
}
