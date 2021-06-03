using JDS;

namespace Client.States
{
    public class MainMenuState : IGameState
    {
        public void OnEnter()
        {
            WindowsManager<WindowTypes>.ShowWindow(WindowTypes.MenuUI);
        }

        public void OnExit()
        {
            
        }
    }
}