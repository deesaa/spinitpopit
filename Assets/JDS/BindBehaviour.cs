using System;
using System.Collections.Generic;
using JDS.NewRC;
using UnityEngine;

namespace JDS
{
    /*public class BindBehaviour<T> : MonoBehaviour
    {
        private readonly List<BindHandler<T>> _bindHandlers = new List<BindHandler<T>>();
        
        private void OnDestroy()
        {
            foreach (var bindHandler in _bindHandlers)
            {
                bindHandler.Destroy();
            }
            AfterDestroy();
        }

        protected void Bind(T valueType, Action action)
        {
            _bindHandlers.Add(GRC<T>.Bind(valueType, action));
        }

        protected virtual void AfterDestroy() { }
    }*/

    public class ObservedBehaviour<T> : MonoBehaviour
    {
        private GroupObservable<T> _groupObservable = new GroupObservable<T>();

        private void Awake()
        {
            RC<T>.Get.Add(_groupObservable);
            _groupObservable.EnableObserver();
            AfterAwake();
        }

        private void OnDestroy()
        {
            RC<T>.Get.Remove(_groupObservable);
            _groupObservable.DisableObserver();
            AfterDestroy();
        }

        protected void Subscribe(T valueType, Action<object> onChanged)
        {
            _groupObservable.Subscribe(valueType, onChanged);
        }

        protected void SetGroupName(string name) => _groupObservable.GroupName = name;
        
        protected virtual void AfterAwake() { }
        protected virtual void AfterDestroy() { }
    }
}