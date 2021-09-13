using System;
using System.Collections.Generic;
using UnityEngine;

namespace JDS
{
    public class UnityReactiveCoreObserver : MonoBehaviour, IReactiveCoreObserver<string>
    {
        private static UnityReactiveCoreObserver _instance;

        public Dictionary<string, ReactiveCoreObserverElement> _valuePairs = new Dictionary<string, ReactiveCoreObserverElement>();

        private void Awake()
        {
            if (_instance != null)
                Destroy(gameObject);
        }

        public void OnKeyValueChanged(string key, object nextValue)
        {
            string value = nextValue != null ? nextValue.ToString() : "NULL_VALUE";
            
            if (_valuePairs.ContainsKey(key))
            {
                _valuePairs[key].SetNext(value);
            }
            else
            {
                _valuePairs.Add(key, new ReactiveCoreObserverElement(key, value));
            }
        }

        public static void Create(ReactiveCore<string> reactiveCore)
        {
            var go = new GameObject("[RC-Debug Observer]");
            var observer = go.AddComponent<UnityReactiveCoreObserver>();
            _instance = observer;
            DontDestroyOnLoad(go);
            reactiveCore.SetDebugObserver(observer);
        }
    }

    public class ReactiveCoreObserverElement
    {
        private static readonly int capacity = 32;
        public string Key { get; private set; }
        public readonly List<DatedValue> ValueHistory = new List<DatedValue>();
        
        public ReactiveCoreObserverElement(string key, string value)
        {
            this.Key = key;
            SetNext(value);
        }

        public void SetNext(string value)
        {
            ValueHistory.Add(new DatedValue(value));
            if (ValueHistory.Count > capacity)
                ValueHistory.RemoveAt(0);
        }
    }

    public struct DatedValue
    {
        public string Value;
        public DateTime DateTime;

        public DatedValue(string value)
        {
            Value = value;
            DateTime = DateTime.Now;
        }
    }
}