using System.Collections.Generic;
using UniTools.Utils;
using UnityEngine;

namespace UniTools.Patterns.ObjectPool
{
    public class ObjectPool<TObject> where TObject : Component
    {
        private readonly TObject _prefab;
        private readonly Transform _root;
        private readonly Queue<TObject> _freeObjects;

        public ObjectPool(TObject prefab, Transform root, int capacity = 0)
        {
            _prefab = prefab;
            _root = root;
            _freeObjects = new Queue<TObject>(capacity);
            
            for (var i = 0; i < capacity; i++)
            {
                InstantiateNewObject();
            }
        }

        public (TObject Obj, DisposableToken ReleaseToken) Get()
        {
            if (_freeObjects.Count == 0) InstantiateNewObject();
            
            var obj = _freeObjects.Dequeue();
            var token = new DisposableToken(() => PoolObject(obj));
            return (obj, token);
        }

        private void PoolObject(TObject obj)
        {
            obj.gameObject.SetActive(false);
            _freeObjects.Enqueue(obj);
        }
        
        private void InstantiateNewObject()
        {
            var instantiated = Object.Instantiate(_prefab, _root);
            PoolObject(instantiated);
        }
    }
}