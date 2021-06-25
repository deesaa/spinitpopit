using Client.States;
using JDS;
using JDS.Messenger;
using UnityEngine;

namespace States
{
    public class SideMenuState : EcsGameState, IMessageReceiver
    {
        protected override void BeforeInit()
        {
            WM<WindowType>.Show(WindowType.SideMenuUI);
            Messenger.Get.EnableReceiver(this);
        }

        protected override void AfterDestroy()
        {
            WM<WindowType>.Hide(WindowType.SideMenuUI);
            Messenger.Get.DisableReceiver(this);
        }
        
        public void ReceiveMessage(MessageHandler message)
        {
            switch (message.Message)
            {
                case "OnBackButton":
                {
                    GSM<StateType>.Get.Unnest();
                    break;
                }
                case "OnLevelsButton":
                {
                    GSM<StateType>.Get.Nest(StateType.SelectLevel);
                    break;
                }
                case "OnSpinnersButton":
                {
                    GSM<StateType>.Get.Nest(StateType.SelectSpinner);
                    break;
                }
            }
        }

        public override void MovedBack()
        {
            WM<WindowType>.Show(WindowType.SideMenuUI);
            Messenger.Get.EnableReceiver(this);
        }

        public override void MovedForward()
        {
            WM<WindowType>.Hide(WindowType.SideMenuUI);
            Messenger.Get.DisableReceiver(this);
        }
    }
}