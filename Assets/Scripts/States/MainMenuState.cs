using JDS;
using Leopotam.Ecs;

namespace Client.States
{
    public class MainMenuState : EcsGameState
    {
        protected override void BeforeInit()
        {
            WM<WindowType>.Show(WindowType.MainMenuUI);
        }

        protected override void AfterDestroy()
        {
            WM<WindowType>.Hide(WindowType.MainMenuUI);
        }

        public override void StateMessage(string name)
        {
            switch (name)
            {
                case "StartBtn":
                    GSM<StateType>.Get.ChangeOn(StateType.Level);
                    break;
            }
        }

        
    }
}