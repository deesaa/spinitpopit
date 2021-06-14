using Client.States;
using JDS;
using UnityEngine;

namespace States
{
    public class SideMenuState : EcsGameState
    {
        protected override void BeforeInit()
        {
            WM<WindowType>.Show(WindowType.SideMenuUI);
        }

        protected override void AfterDestroy()
        {
            WM<WindowType>.Hide(WindowType.SideMenuUI);
        }
        
        public override void StateMessage(string name)
        {
            switch (name)
            {
                case "OnBackBtn":
                {
                    GSM<StateType>.Get.Unnest();
                    break;
                }
            }
        }
    }
}