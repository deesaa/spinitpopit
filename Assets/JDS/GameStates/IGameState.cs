using System.Collections.Generic;

namespace JDS
{
    public interface IGameState
    {
        void OnEnter();
        void OnExit();
        void StateMessage(string name);
    }

    public abstract class EcsGameState : IGameState
    {
        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public virtual void StateMessage(string name) { }
    }
}