using System;
using System.Collections.Generic;
using UnityEngine;

namespace JDS
{
    public class ReactiveCore : MonoBehaviour
    {
        public static ReactiveCore Instance;
        
        private readonly Dictionary<string, object> _objects 
            = new Dictionary<string, object>();
        
        private readonly Dictionary<string, List<Action>> _subscriptions 
            = new Dictionary<string, List<Action>>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void Set(string key, object value, bool react = true)
        {
            Debug.Log($"Set {key}");
            if(_objects.ContainsKey(key))
                _objects[key] = value;   
            else 
                _objects.Add(key, value);

            if (react)
                React(key);
        }

        public void React(string key)
        {
            if(_subscriptions.ContainsKey(key))
                _subscriptions[key].ForEach(x=>x());
            else
                Debug.Log($"Key {key} has no subscriptions");
        }

        public T Get<T>(string key)
        {
            if (_objects.ContainsKey(key))
            {
                if (_objects[key] is T value) return value;
                Debug.Log($"Value with key {key} can not be casted to {typeof(T)}");
                return default;
            }
        
            Debug.Log($"Key {key} is not defined");
            return default;
        }

        public void SubscribeKey(string key, Action action)
        {
            if(!_subscriptions.ContainsKey(key))
                _subscriptions.Add(key, new List<Action>() { action });
            else 
                _subscriptions[key].Add(action);
        }

        public void UnsubscribeKey(string key, Action action)
        {
            if(_subscriptions[key] == null) return;
            _subscriptions[key].Remove(action);
        }
    
        public void UnsubscribeKeyAll(string key)
        { 
            if(_subscriptions[key] == null) return;
            _subscriptions[key].Clear();
        }
    }
    
    public static class RC
    {
        public static ReactiveCore Get => ReactiveCore.Instance;
    }
}