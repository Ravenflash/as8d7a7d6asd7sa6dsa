using UnityEngine;

namespace Ravenflash.Patterns
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance;
        public static T Instance
        {
            get
            {
                if (!_instance) _instance = FindObjectOfType<T>();
                return _instance;
            }
            private set => _instance = value;
        }

        protected virtual void Awake()
        {
            if (!_instance) _instance = this as T;

            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        protected virtual void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }

    }
}
