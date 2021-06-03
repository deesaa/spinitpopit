using System;
using System.Collections.Generic;
using Client.ReactiveValues;
using UnityEngine;

namespace JDS
{
    public static class ReactiveCoreG<T> where T : System.Enum
    {
        private static readonly Dictionary<T, object> _objects 
            = new Dictionary<T, object>();
        
        private static readonly Dictionary<T, List<Action>> _subscriptions 
            = new Dictionary<T, List<Action>>();

        public static void Set(T key, object value, bool react = true)
        {
            //Debug.Log($"Set {key}");
            if(_objects.ContainsKey(key))
                _objects[key] = value;   
            else 
                _objects.Add(key, value);

            if (react)
                React(key);
        }

        public static void React(T key)
        {
            if(_subscriptions.ContainsKey(key))
                _subscriptions[key].ForEach(x=>x());
            else
                Debug.Log($"Key {key} has no subscriptions");
        }

        public static TV Get<TV>(T key)
        {
            if (_objects.ContainsKey(key))
            {
                if (_objects[key] is TV value) return value;
                Debug.Log($"Value with key {key} can not be casted to {typeof(T)}, new created with this type");
                return default;
            }
        
            Debug.Log($"Key {key} is not defined, created now");
            return default;
        }
        

        public static void Bind(T key, Action action)
        {
            if(!_subscriptions.ContainsKey(key))
                _subscriptions.Add(key, new List<Action>() { action });
            else 
                _subscriptions[key].Add(action);
        }

        public static void Unbind(T key, Action action)
        {
            if(_subscriptions[key] == null) return;
            _subscriptions[key].Remove(action);
        }
    
        public static void UnbindAll(T key)
        { 
            if(_subscriptions[key] == null) return;
            _subscriptions[key].Clear();
        }

        public static void Change<TV>(T key, Func<TV, TV> change) where TV : struct
        { 
            Set(key, change(Get<TV>(key)));
        }
        
        public static void Change<TV>(T key, Action<TV> change) where TV : class
        {
            change(Get<TV>(key));
            React(key);
        }
    }
}