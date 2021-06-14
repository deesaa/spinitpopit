using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace JDS
{
    public abstract class EcsGameState : IGameState
    {
        protected EcsWorld World { get; private set; }

        private readonly List<IEcsInitSystem> _stateInitSystems = new List<IEcsInitSystem>();  
        private readonly List<IEcsDestroySystem> _stateDestroySystems = new List<IEcsDestroySystem>();
        private readonly List<object> _inject = new List<object>();

        private EcsSystems _stateSystems;
        
        public void SetWorld(EcsWorld world) => World = world;

        public EcsGameState Add(IEcsInitSystem stateInitSystems)
        {
            _stateInitSystems.Add(stateInitSystems);
            return this;
        }
            
        public EcsGameState Add(IEcsDestroySystem stateDestroySystem)
        {
            _stateDestroySystems.Add(stateDestroySystem);
            return this;
        }
        
        public EcsGameState Inject(object o)
        {
            _inject.Add(o);
            return this;
        }
        
        protected virtual void BeforeInit() { }
        
        public void OnEnter()
        {
            BeforeInit();
            
            _stateSystems = new EcsSystems(World);
            
            foreach (var initSystem in _stateInitSystems)
                _stateSystems.Add(initSystem);

            foreach (var destroySystem in _stateDestroySystems)
                _stateSystems.Add(destroySystem);
            
            foreach (object o in _inject)
                _stateSystems.Inject(o);

            _stateSystems.Init();
            
            AfterInit();
        }
        
        protected virtual void AfterInit() { }
        
        protected virtual void BeforeDestroy() { }
        
        public void OnExit()
        {
            BeforeDestroy();
            _stateSystems.Destroy();
            _stateSystems = null;
            AfterDestroy();
        }
        
        protected virtual void AfterDestroy() { }
        public virtual void StateMessage(string name) { }
        public virtual void MovedForward() { }
        public virtual void MovedBack() { }
    }
}