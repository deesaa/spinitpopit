using System;
using System.Collections.Generic;
using Leopotam.Ecs;

namespace JDS
{
    public abstract class EcsObserveSystem<T> : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly List<BindHandler<T>> _bindHandlers = new List<BindHandler<T>>();
        
        protected void Bind(T valueType, Action<object> action, ReactiveCore<T> parent)
        {
            _bindHandlers.Add(parent.Bind(valueType, action));
        }
        
        public abstract void Init();

        public void Destroy()
        {
            foreach (var bindHandler in _bindHandlers)
            {
                bindHandler.Destroy();
            }
            AfterUnbindOnDestroy();
        }
        
        protected virtual void AfterUnbindOnDestroy() { }
    }
}