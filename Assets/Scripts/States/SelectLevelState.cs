using System;
using JDS;

namespace Client.States
{
    public class SelectLevelState : EcsGameState, IMessageReceiver
    {
        protected override void BeforeInit()
        {
           WM<WindowType>.Show(WindowType.SelectLevelUI);
           Messenger.Get.EnableReceiver(this);
        }

        protected override void AfterDestroy()
        {
            WM<WindowType>.Hide(WindowType.SelectLevelUI);
            Messenger.Get.DisableReceiver(this);
        }

        public void ReceiveMessage(MessageHandler message)
        {
            switch (message.Message)
            {
                case "OnLevelClick":
                    break;
            }
        }
    }
}