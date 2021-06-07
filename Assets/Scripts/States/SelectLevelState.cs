using JDS;

namespace Client.States
{
    public class SelectLevelState : GameStateEcs
    {
        public override void OnEnter()
        {
           WM<WindowType>.ShowWindow(WindowType.SelectLevelUI);
        }

        public override void OnExit()
        {
            WM<WindowType>.HideWindow(WindowType.SelectLevelUI);
        }

        public override void OnEvent(string name)
        {
            switch (name)
            {
                case "OnLevelClick":
                    
                    break;
            }
        }
    }
}