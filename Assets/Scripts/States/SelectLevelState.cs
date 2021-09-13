using System;
using Client.ReactiveValues;
using DG.Tweening;
using JDS;
using JDS.Messenger;

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
                case "OnBackButton":
                {
                    GSM<StateType>.Get.Unnest();
                    break;
                }
                case "OnLevelClick":
                {
                    Model.Get.Set("NextState", StateType.Level);
                    //RC<RValueType>.Set(RValueType.NextState, StateType.Level);
                    GSM<StateType>.Get.ChangeOn(StateType.Transition);
                    break;
                }
            }
        }
        
    }
}