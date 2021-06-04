using JDS;

namespace Client.States
{
    public class LevelState : IGameState
    {
        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }

        public void OnEvent(string name)
        {
            switch (name)
            {
                case "PlayerDeath":
                    GSM<StateType>.ChangeOn(StateType.MainMenu);
                    break;
            }
        }
    }
}