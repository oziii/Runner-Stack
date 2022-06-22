using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OziLib
{
    public sealed class EventManager : MonoBehaviour
    {
        private Dictionary<string, UnityEvent<object>> eventDictionary;

        private static EventManager _eventManager;

        public static EventManager instance
        {
            get
            {
                if (!_eventManager)
                {
                    _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    if (!_eventManager)
                    {
                        Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene");
                    }
                    else
                    {
                        _eventManager.Init();
                    }
                }

                return _eventManager;
            }
        }

        private void Init()
        {
            eventDictionary ??= new Dictionary<string, UnityEvent<object>>();
        }

        public static void StartListening(string eventName, UnityAction<object> listener)
        {
            if (instance.eventDictionary.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent<object>();
                thisEvent.AddListener(listener);
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, UnityAction<object> listener)
        {
            if (_eventManager == null) return;
            if (instance.eventDictionary.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(string eventName, object data)
        {
            if (instance.eventDictionary.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent?.Invoke(data);
            }
        }
    }
}