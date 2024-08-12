using UnityEngine;

namespace UniTools.Patterns.Singletons
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        var go = new GameObject(typeof(T).Name + " Auto-Generated");
                        _instance = go.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }
        
        public static bool HasInstance => _instance != null;
        public static T TryGetInstance() => HasInstance ? _instance : null;
        
        private void Awake()
        {
            InitializeSingleton();
            AwakeInternal();
        }

        private void InitializeSingleton()
        {
            if (!Application.isPlaying) return;

            _instance = this as T;
        }

        protected virtual void AwakeInternal() { }
    }
}