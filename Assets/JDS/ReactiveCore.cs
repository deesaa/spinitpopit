using System;
using System.Collections.Generic;

namespace JDS
{

    public interface IReactiveCoreObserver<T>
    {
        void OnKeyValueChanged(T key, object nextValue);
    }


    /// <summary>
    /// Reactive Core
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReactiveCore<T>
    {
        private  readonly Dictionary<T, object> _objects = new Dictionary<T, object>();
        
        private  readonly Dictionary<T, List<BindHandler<T>>> _subscriptions = new Dictionary<T, List<BindHandler<T>>>();

        private IReactiveCoreObserver<T> _debugObserver;

        public  void Set(T key, object value)
        {
            if(_objects.ContainsKey(key))
                _objects[key] = value;   
            else 
                _objects.Add(key, value);
            
            if(_debugObserver != null)
                _debugObserver.OnKeyValueChanged(key, value);
            
            React(key, value);
        }

        public void React(T key, object value)
        {
            if(_subscriptions.ContainsKey(key))
                _subscriptions[key].ForEach(x => x.Invoke(value));
            else
                DebugLog.Log($"Key {key} has no subscriptions");
        }

        public TV Get<TV>(T key)
        {
            if (_objects.ContainsKey(key))
            {
                if (_objects[key] == null) return default;
                if (_objects[key] is TV value) return value;
                DebugLog.LogWarning($"Value with key {key} can not be casted to {typeof(T)}, new created now with this type");
                return CreateAndSetNew<TV>(key);
            }
            DebugLog.LogWarning($"Key {key} is not defined, new created now");
            
            return CreateAndSetNew<TV>(key);
        }

        private TV CreateAndSetNew<TV>(T key)
        {
            TV newValue = default;
            Set(key, newValue);
            return newValue;
        }

        public BindHandler<T> Bind(T key, Action<object> action)
        {
            BindHandler<T> handler = new BindHandler<T>(action, key, this);

            if (!_subscriptions.ContainsKey(key))
                _subscriptions.Add(key, new List<BindHandler<T>> { handler });
            else
                _subscriptions[key].Add(handler);

            return handler;
        }

        public void Unbind(T key, Action<object> action)
        {
            _subscriptions[key]?.RemoveAll(handler => handler.IsEqual(key, action));
        }
    
        public void UnbindAll(T key)
        {
            _subscriptions[key]?.Clear();
        }

        public void Change<TV>(T key, Func<TV, TV> change) where TV : struct
        { 
            Set(key, change(Get<TV>(key)));
        }
        
        /*public void Change<TV>(T key, Action<TV> change) where TV : class
        {
            change(Get<TV>(key));
            React(key);
        }*/
        
        public void SetDebugObserver(IReactiveCoreObserver<T> reactiveCoreObserver)
        {
            _debugObserver = reactiveCoreObserver;
        }
    }

    public readonly struct BindHandler<T>
    {
        private readonly Action<object> _action;
        private readonly T _valueType;
        private readonly ReactiveCore<T> _parentReactiveCore;

        public BindHandler(Action<object> action, T valueType, ReactiveCore<T> parentReactiveCore)
        {
            _action = action;
            _valueType = valueType;
            _parentReactiveCore = parentReactiveCore;
        }

        public void Invoke(object value) => _action(value);

        public void Destroy()
        {
            _parentReactiveCore.Unbind(_valueType, _action);
        }

        public bool IsEqual(T valueType, Action<object> action)
        {
            return _action == action && valueType.Equals(_valueType);
        }
    }
}