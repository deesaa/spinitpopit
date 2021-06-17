using System;
using Client.ReactiveValues;
using JDS;
using JDS.Messenger;

namespace Client.States
{
    public class SelectLevelState : EcsGameState<RValueType>
    {
        protected override void BeforeInit()
        {
           WM<WindowType>.Show(WindowType.SelectLevelUI);
           Subscribe(RValueType.OnLevelClick, OnLevelClick);
           EnableObserver();
        }

        protected override void AfterDestroy()
        {
            WM<WindowType>.Hide(WindowType.SelectLevelUI);
            DisableObserver();
            Dispose();
        }

        private void OnLevelClick(object value)
        {
            
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