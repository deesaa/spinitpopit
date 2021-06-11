using System.Collections.Generic;
using Leopotam.Ecs;

namespace JDS
{
    public interface IGameState
    {
        void OnEnter();
        void OnExit();
        void Update();
        void OnEvent(string name);
    }

    public abstract class GameStateEcs : IGameState
    {
        protected EcsWorld World { get; private set; }

        private List<object> _injected = new List<object>();

        public void SetWorld(EcsWorld world)
        {
            World = world;
        }
        
        public GameStateEcs Inject(object o)
        {
            _injected.Add(o);
            return this;
        }

        protected void InjectIn(EcsSystems systems)
        {
            foreach (object o in _injected)
            {
                systems.Inject(o);
            }
        }
        
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void Update() { }
        public virtual void OnEvent(string name) { }
    }
}