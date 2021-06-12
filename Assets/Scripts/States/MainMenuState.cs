using JDS;
using Leopotam.Ecs;

namespace Client.States
{
    public class MainMenuState : GameStateEcs
    {
        public override void OnEnter()
        {
            WM<WindowType>.Show(WindowType.MainMenuUI);
        }

        public override void OnExit()
        {
            WM<WindowType>.Hide(WindowType.MainMenuUI);
        }

        public override void StateMessage(string name)
        {
            switch (name)
            {
                case "StartBtn":
                    GSM<StateType>.ChangeOn(StateType.Level);
                    break;
            }
        }

        
    }
}