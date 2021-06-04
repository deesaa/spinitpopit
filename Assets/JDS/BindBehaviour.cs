using System;
using UnityEngine;

namespace JDS
{
    public class BindBehaviour<T> : MonoBehaviour where T : Enum 
    {
        private void OnDestroy()
        {
            
            
            AfterDestroy();
        }

        protected void Bind(T valueType)
        {
            
        }

        protected virtual void AfterDestroy() { }
    }
}