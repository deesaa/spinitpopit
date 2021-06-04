using JDS;

namespace Client.States
{
    public class MainMenuState : IGameState
    {
        public void OnEnter()
        {
            //WM<WindowType>.HideAll();
            WM<WindowType>.ShowWindow(WindowType.MainMenuUI);
        }

        public void OnExit()
        {
            WM<WindowType>.HideWindow(WindowType.MainMenuUI);
        }

        public void OnEvent(string name)
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