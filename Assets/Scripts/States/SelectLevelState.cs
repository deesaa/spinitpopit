using JDS;

namespace Client.States
{
    public class SelectLevelState : IGameState
    {
        public void OnEnter()
        {
           WM<WindowType>.Show(WindowType.SelectLevelUI);
        }

        public void OnExit()
        {
            WM<WindowType>.Hide(WindowType.SelectLevelUI);
        }

        public void StateMessage(string name)
        {
            switch (name)
            {
                case "OnLevelClick":
                    break;
            }
        }
    }
}