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
    public static class GRC<T>
    {
        private static readonly Dictionary<T, object> _objects 
            = new Dictionary<T, object>();
        
        private static readonly Dictionary<T, List<BindHandler<T>>> _subscriptions 
            = new Dictionary<T, List<BindHandler<T>>>();

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
                _subscriptions[key].ForEach(x => x.Invoke());

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
                Debug.LogWarning($"Value with key {key} can not be casted to {typeof(T)}, new created now with this type");
#endif
                return CreateAndSetNew<TV>(key);
            }
#if UNITY_EDITOR
            Debug.LogWarning($"Key {key} is not defined, new created now");
#endif
            return CreateAndSetNew<TV>(key);
        }

        private static TV CreateAndSetNew<TV>(T key)
        {
            TV newValue = default;
            Set(key, newValue);
            return newValue;
        }

        public static BindHandler<T> Bind(T key, Action action)
        {
            BindHandler<T> handler = new BindHandler<T>(action, key);

            if (!_subscriptions.ContainsKey(key))
                _subscriptions.Add(key, new List<BindHandler<T>>() { handler });
            else
                _subscriptions[key].Add(handler);

            return handler;
        }

        public static void Unbind(T key, Action action)
        {
            _subscriptions[key]?.RemoveAll(handler => handler.IsEqual(key, action));
        }
    
        public static void UnbindAll(T key)
        {
            _subscriptions[key]?.Clear();
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

    public readonly struct BindHandler<T>
    {
        private readonly Action _action;
        private readonly T _valueType;

        public BindHandler(Action action, T valueType)
        {
            _action = action;
            _valueType = valueType;
        }

        public void Invoke() => _action();

        public void Destroy()
        {
            GRC<T>.Unbind(_valueType, _action);
        }

        public bool IsEqual(T valueType, Action action)
        {
            return _action == action && valueType.Equals(_valueType);
        }
    }
}