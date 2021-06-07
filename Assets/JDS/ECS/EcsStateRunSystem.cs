using System;
using System.Collections.Generic;
using Leopotam.Ecs;

namespace JDS
{
    public abstract class EcsStateRunSystem<T> : IEcsRunSystem where T : Enum
    {
        private readonly List<T> _stateTypes = new List<T>();

        public void Run()
        {
            if (_stateTypes.Count <= 0)
            {
                OnRun();
                return;
            }
            
            if(!_stateTypes.Contains(GSM<T>.CurrentStateType)) return;
            OnRun();
        }

        protected abstract void OnRun();

        public EcsStateRunSystem<T> AddState(T stateType)
        {
            _stateTypes.Add(stateType);
            return this;
        }
        
    }
}