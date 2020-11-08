using System;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Assets.Scripts.Services
{
    public class ObjectPool
    {
        private readonly Transform _rootContainer;
        private readonly Transform _stashedObjectsContainer;
        private readonly Transform _activeObjectsContainer;

        private readonly Dictionary<MonoBehaviour, MonoBehaviour> _poolContainers = new Dictionary<MonoBehaviour, MonoBehaviour>();
        private readonly Dictionary<Type, Dictionary<MonoBehaviour, Queue<MonoBehaviour>>> _pool = new Dictionary<Type, Dictionary<MonoBehaviour, Queue<MonoBehaviour>>>();

        public ObjectPool()
        {
            _rootContainer = new GameObject().transform;
            _rootContainer.name = nameof(ObjectPool);

            _activeObjectsContainer = new GameObject().transform;
            _activeObjectsContainer.name = "active";
            _activeObjectsContainer.parent = _rootContainer;

            _stashedObjectsContainer = new GameObject().transform;
            _stashedObjectsContainer.name = "stashed";
            _stashedObjectsContainer.gameObject.SetActive(false);
            _stashedObjectsContainer.parent = _rootContainer;

        }

        public void Stash<T>(T value) where T : MonoBehaviour, IClone<T>
        {
            var type = typeof(T);

            if (!_pool.ContainsKey(type))
            {
                _pool.Add(type, new Dictionary<MonoBehaviour, Queue<MonoBehaviour>>());
            }

            T original = value.GetOriginal();

            if (!_pool[type].ContainsKey(original))
            {
                _pool[type].Add(original, new Queue<MonoBehaviour>());
            }

            if (!_pool[type][original].Contains(value))
            {
                _pool[type][original].Enqueue(value);
            }

            value.transform.parent = _stashedObjectsContainer;
        }

        public T Get<T>(T original) where T : MonoBehaviour, IClone<T>
        {
            T result;
            Type type = original.GetType();
            bool hasObjects = _pool.ContainsKey(type) && _pool[type].ContainsKey(original) && _pool[type][original].Count > 0;

            if (hasObjects)
            {
                result = _pool[type][original].Dequeue() as T;
            }
            else
            {
                result = UnityEngine.Object.Instantiate(original);
                result.SetOriginal(original);
            }

            result.transform.parent = _activeObjectsContainer;
            result.transform.localScale = result.GetOriginal().transform.localScale;
            result.Reset();

            return result;
        }
    }
}
