using System;
using DG.Tweening;
using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.States
{
    public class MainMenuState : EcsGameState, IMessageReceiver
    {
        private bool _interactable = false;
        
        protected override void BeforeInit()
        {
            //_interactable = false;
            WM<WindowType>.Show(WindowType.MainMenuUI);

            DOVirtual.DelayedCall(0.3f, () =>
            {
                //_interactable = true;
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
            //if(!_interactable)
            //    return;
            
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