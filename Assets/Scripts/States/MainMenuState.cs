using JDS;

namespace Client.States
{
    public class MainMenuState : IGameState
    {
        public void OnEnter()
        {
            WindowsManager<WindowTypes>.ShowWindow(WindowTypes.MainMenuUI);
        }

        public void OnExit()
        {
            WindowsManager<WindowTypes>.HideWindow(WindowTypes.MainMenuUI);
        }

        public void OnEvent(string name)
        {
            switch (name)
            {
                case "StartBtn":
                    GameStatesManager<StateTypes>.ChangeOn(StateTypes.Level);
                    break;
            }
        }
    }
}