using System;
using System.Collections.Generic;
using UnityEngine;

namespace JDS
{
    public class BindBehaviour<T> : MonoBehaviour where T : Enum
    {
        private List<BindHandler<T>> _bindHandlers = new List<BindHandler<T>>();
        
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
    }
}