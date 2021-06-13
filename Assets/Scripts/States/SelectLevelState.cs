using JDS;

namespace Client.States
{
    public class SelectLevelState : EcsGameState
    {
        protected override void BeforeInit()
        {
           WM<WindowType>.Show(WindowType.SelectLevelUI);
        }

        protected override void AfterDestroy()
        {
            WM<WindowType>.Hide(WindowType.SelectLevelUI);
        }

        public override void StateMessage(string name)
        {
            switch (name)
            {
                case "OnLevelClick":
                    break;
            }
        }
    }
}