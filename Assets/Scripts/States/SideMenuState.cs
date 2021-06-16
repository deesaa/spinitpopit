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
                case "OnBackBtn":
                {
                    GSM<StateType>.Get.Unnest();
                    break;
                }
            }
        }
    }
}