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

        public void SetWorld(EcsWorld world)
        {
            World = world;
        }
        
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public void Update() { }
        public virtual void OnEvent(string name) { }
    }
}