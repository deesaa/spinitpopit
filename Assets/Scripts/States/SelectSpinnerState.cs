using Client.ReactiveValues;
using Client.States;
using JDS;
using JDS.Messenger;

namespace States
{
    public class SelectSpinnerState : EcsGameState, IMessageReceiver
    {
        protected override void BeforeInit()
        {
            WM<WindowType>.Show(WindowType.SelectSpinnerUI);
            Messenger.Get.EnableReceiver(this);
        }

        protected override void AfterDestroy()
        {
            WM<WindowType>.Hide(WindowType.SelectSpinnerUI);
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
                case "OnSpinnerClick":
                {
                    RC<RValueType>.Set(RValueType.NextState, StateType.Level);
                    GSM<StateType>.Get.ChangeOn(StateType.Transition);
                    break;
                }
            }
        }

    }
}