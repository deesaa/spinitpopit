using System;
using System.Collections.Generic;
using Client.ReactiveValues;
using UnityEngine;

namespace JDS
{
    /// <summary>
    /// Generic Reactive Core
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class GRC<T> where T : System.Enum
    {
        private static readonly Dictionary<T, object> _objects 
            = new Dictionary<T, object>();
        
        private static readonly Dictionary<T, List<Action>> _subscriptions 
            = new Dictionary<T, List<Action>>();

        public static void Set(T key, object value)
        {
            if(_objects.ContainsKey(key))
                _objects[key] = value;   
            else 
                _objects.Add(key, value);
            
            React(key);
        }

        public static void React(T key)
        {
            if(_subscriptions.ContainsKey(key))
                _subscriptions[key].ForEach(x=>x());

#if UNITY_EDITOR
            else
                Debug.Log($"Key {key} has no subscriptions");
#endif
        }

        public static TV Get<TV>(T key)
        {
            if (_objects.ContainsKey(key))
            {
                if (_objects[key] is TV value) return value;
#if UNITY_EDITOR
                Debug.Log($"Value with key {key} can not be casted to {typeof(T)}, new created now with this type");
#endif
                return CreateAndSetNew<TV>(key);
            }
#if UNITY_EDITOR
            Debug.Log($"Key {key} is not defined, new created now");
#endif
            return CreateAndSetNew<TV>(key);
        }

        private static TV CreateAndSetNew<TV>(T key)
        {
            TV newValue = default;
            Set(key, newValue);
            return newValue;
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

    public struct BindHandler<T> where T : Enum
    {
        private Action _action;
        private T _valueType;
        public BindHandler(Action action, T valueType)
        {
            _action = action;
            _valueType = valueType;
        }
        
    }
}