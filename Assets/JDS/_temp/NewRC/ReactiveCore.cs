
/*
using System;
using System.Collections.Generic;

namespace JDS.NewRC
{
    public static class RC_
    {
        public static RC<TestValueType> Get = new RC<TestValueType>();
    }

    public static class GlobalOG
    {
        public static ObservableGroup<TestValueType> Get;
        static GlobalOG()
        {
            Get = RC_.Get.Create();
        }
    }
    
    public class RC<T>
    {
        private List<ObservableGroup<T>> _observableGroups = new List<ObservableGroup<T>>();
        
        public static RC<T> Get = new RC<T>();

        public ObservableGroup<T> Create()
        {
            var observableGroup = new ObservableGroup<T>();
            _observableGroups.Add(observableGroup);
            return observableGroup;
        }

        public void Push(T valueType)
        {
            
        }

        public void Dispose(ObservableGroup<T> observableGroup)
        {
            _observableGroups.Find(x => x == observableGroup).Dispose();
        }
    } 

    public class TestRC
    {
        public void Test()
        {
            RC_.Get.Create()
                .Subscribe(TestValueType.Value_1, Value1C)
                .Subscribe(TestValueType.Value_2, Value2C);
        }

        public void Value1C(object o)
        {
            
        }
        
        public void Value2C(object o)
        {
            
        }
    }
    
    public class ObservableGroup<T> : IDisposable
    {
        private List<T> _subscribedKeys = new List<T>();
        private Dictionary<T, ObservableKey> _observableKeys = new Dictionary<T, ObservableKey>();
        
        public bool IsActive;

        public ObservableGroup<T> Subscribe(T key, Action<object> onChange)
        {
            if(!_subscribedKeys.Contains(key))
                _subscribedKeys.Add(key);

            if (_observableKeys.ContainsKey(key))
                _observableKeys[key].Subscribe(onChange);
            else
                _observableKeys.Add(key, new ObservableKey().Subscribe(onChange));

            return this;

        }

        public void Override(T key, object value)
        {
            if(_subscribedKeys.Contains(key))
                _observableKeys[key].Override(value, IsActive);
        }

        public void Push(T key, object value)
        {
            if(_subscribedKeys.Contains(key))
                _observableKeys[key].Push(value, IsActive);
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

        public void Dispose()
        {
            _observableKeys.Clear();
            foreach (var observableKey in _observableKeys)
            {
                observableKey.Value.Dispose();
            }
            _subscribedKeys.Clear();
        }
    }

    public class ObservableKey : IDisposable
    {
        private Queue<object> _notReceivedChanges = new Queue<object>();
        private List<Action<object>> _subscriptionsOnKeyChange = new List<Action<object>>();
        private object _lastEnqueuedValue;
        
        public ObservableKey Subscribe(Action<object> onChange)
        {
            _subscriptionsOnKeyChange.Add(onChange);
            return this;
        }
        
        private void Send()
        {
            foreach (var subscription in _subscriptionsOnKeyChange)
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

        public void Override(object value, bool canSend)
        {
            _notReceivedChanges.Clear();
            Enqueue(value);
            if(canSend)
                Send();
        }

        public void Push(object value,bool canSend)
        {
            Enqueue(value);
            if(canSend)
                Send();
        }

        public void Dispose()
        {
            _notReceivedChanges.Clear();
            _subscriptionsOnKeyChange.Clear();
            _lastEnqueuedValue = null;
        }
    }

    public enum TestValueType
    {
        Value_1,
        Value_2
    }
}
*/