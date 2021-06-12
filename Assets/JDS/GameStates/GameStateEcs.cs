using Leopotam.Ecs;

namespace JDS
{
    public abstract class GameStateEcs : IGameState
    {
        protected EcsWorld World { get; private set; }
        
        public void SetWorld(EcsWorld world)
        {
            World = world;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void StateMessage(string name) { }
    }
}