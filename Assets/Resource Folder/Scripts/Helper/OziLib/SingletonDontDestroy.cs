using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace OziLib
{
    public class SingletonDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool _isDestroyed;

        public static T instance
        {
            get
            {
                if (_isDestroyed)
                {
                    return null;
                }

                if (_instance == null)
                {
                    _instance = (T) FindObjectOfType(typeof(T));
                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                                       " - there should never be more then 1 singletion!" +
                                       " Reopening the scene might fix it. Thread: " + Thread.CurrentThread.Name);
                        return _instance;
                    }
                    
                    // If client code try to access this class before it is instantiated
                    // by Unity system, this code results creating an empty GameObject. has null references
                    // which are assigned at design-time.

                    //if (_instance == null) {
                    //    GameObject singleton = new GameObject();
                    //    singleton.AddComponent<T>();
                    //    singleton.name = "(singleton) " + typeof(T).ToString();
                    //    Debug.LogFormat("[Singleton] An instance of '{0}' is initialized via code. Thread: {1}", typeof(T),
                    //                                                                Thread.CurrentThread.ManagedThreadId);
                    //    //DontDestroyOnLoad(singleton);
                    //} else {
                    //    Debug.Log("[Singleton] Using instance already created: " + _instance.gameObject.name);
                    //}
                }

                return _instance;
            }
        }

        virtual protected void Awake()
        {
            string name = typeof(T).Name;
            if (_instance == null)
            {
                _instance = GetComponent<T>();
                DontDestroyOnLoad(gameObject);
                Debug.Log($"[{name}::Awake] SingletonDontDestroy object initialized.");
            }
            else
            {
                if (gameObject.GetComponents<Component>().Length > 1)
                {
                    Debug.Log(
                        $"[{name}::Awake] '{name}' already created! GameObject has other components, so just destroying newly created component.");
                    DestroyImmediate(this);
                }
                else
                {
                    Debug.Log($"[{name}::Awake] '{gameObject.name}' already created! Destroying newly created one");
                    DestroyImmediate(gameObject);
                }
            }
        }

        virtual protected void OnDestroy()
        {
        }

        private void OnApplicationQuit()
        {
            _isDestroyed = true;
        }
    }
}