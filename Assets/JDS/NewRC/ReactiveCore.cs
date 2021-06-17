using System;
using System.Collections.Generic;

namespace JDS.NewRC
{

    public class TestRC
    {
        
        public void Test()
        {
            
            
        }
    }
    
    public class RC<T>
    {
        public static RC<T> Get = new RC<T>();
    }

    public class ObservableGroup<T>
    {
        private Dictionary<T, ObservableKey> _observableKeys = new Dictionary<T, ObservableKey>();
        private Dictionary<T, List<Action<object>>> _subscriptionsOnKeyChange = new Dictionary<T, List<Action<object>>>();

        public bool IsActive;

        public void Subscribe(T key, Action<object> onChange)
        {
            
        }

        public void Override(T key, object value)
        {
            if (TryGetBind(key, out ObservableKey observableKey, out List<Action<object>> subscriptions))
            {
                observableKey.Override(value, subscriptions, IsActive);
            }
        }

        public void Push(T key, object value)
        {
            if (TryGetBind(key, out ObservableKey observableKey, out List<Action<object>> subscriptions))
            {
                observableKey.Push(value, subscriptions, IsActive);
            }
        }

        public bool TryGetBind(T key, out ObservableKey observableKey, out List<Action<object>> actions)
        {
            if (_subscriptionsOnKeyChange.ContainsKey(key))
            {
                actions = _subscriptionsOnKeyChange[key];
                if (actions == null)
                {
                    observableKey = null;
                    return false;
                }
            }
            else
            {
                observableKey = null;
                actions = null;
                return false;
            }

            if (_observableKeys.ContainsKey(key))
            {
                observableKey = _observableKeys[key];
                if (observableKey == null)
                {
                    return false;
                }
            }
            else
            {
                observableKey = null;
                actions = null;
                return false;
            }

            return true;
        }
    }

    public class ObservableKey
    {
        private Queue<object> _notReceivedChanges = new Queue<object>();
        private object _lastEnqueuedValue;
        private void Send(List<Action<object>> subscriptions)
        {
            foreach (var subscription in subscriptions)
            {
                while (TryDequeue(out object value))
                {
                    subscription.Invoke(value);
                }
            }
        }
        private bool TryDequeue(out object value)
        {
            if (_notReceivedChanges.Count > 0)
            {
                value = _notReceivedChanges.Dequeue();
                return true;
            }
            value = null;
            return false;
        }

        private void Enqueue(object value)
        {
            _notReceivedChanges.Enqueue(value);
            _lastEnqueuedValue = value;
        }

        public void Override(object value, List<Action<object>> subscriptions, bool canSend)
        {
            _notReceivedChanges.Clear();
            Enqueue(value);
            if(canSend)
                Send(subscriptions);
        }

        public void Push(object value, List<Action<object>> subscriptions, bool canSend)
        {
            Enqueue(value);
            if(canSend)
                Send(subscriptions);
        }
    }

    public enum TestValueType
    {
        Value_1
    }

    public static class RC_
    {
        public static RC<TestValueType> Get = new RC<TestValueType>();
    }
}