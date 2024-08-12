using UnityEngine;

namespace UniTools.Patterns.Singletons
{
    public class RegulatorSingleton<T> : MonoBehaviour where T : Component
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

        public float InitializationTime { get; private set; }
        
        private void Awake()
        {
            InitializeSingleton();
            AwakeInternal();
        }

        private void InitializeSingleton()
        {
            if (!Application.isPlaying) return;
            InitializationTime = Time.time;
            DontDestroyOnLoad(gameObject);

            var oldInstances = FindObjectsByType<T>(FindObjectsSortMode.None);
            foreach (T old in oldInstances)
            {
                if (old.GetComponent<RegulatorSingleton<T>>().InitializationTime < InitializationTime)
                {
                    Destroy(old.gameObject);
                }
            }

            if (_instance == null)
            {
                _instance = this as T;
            }
        }
        
        protected virtual void AwakeInternal() { }
    }
}