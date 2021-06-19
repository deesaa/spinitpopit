using System;
using DG.Tweening;
using JDS;
using JDS.Messenger;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.States
{
    public class MainMenuState : EcsGameState, IMessageReceiver
    {
        protected override void BeforeInit()
        {
            WM<WindowType>.Show(WindowType.MainMenuUI);

            DOVirtual.DelayedCall(0.3f, () =>
            {
                Messenger.Get.EnableReceiver(this);
            });
        }

        protected override void AfterDestroy()
        {
            WM<WindowType>.Hide(WindowType.MainMenuUI);
            Messenger.Get.DisableReceiver(this);
        }
        
        public void ReceiveMessage(MessageHandler message)
        {
            switch (message.Message)
            {
                case "StartBtn":
                    GSM<StateType>.Get.ChangeOn(StateType.Level);
                    break;
                
                case "SideMenuBtn":
                    GSM<StateType>.Get.Nest(StateType.SideMenu);
                    break;
            }
        }
    }
}