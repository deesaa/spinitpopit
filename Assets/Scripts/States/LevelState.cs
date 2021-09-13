using System;
using Client.Components;
using Client.ReactiveValues;
using Client.Systems;
using Client.UnityComponents;
using Components;
using DG.Tweening;
using DG.Tweening.Core;
using JDS;
using JDS.Messenger;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.States
{
    public class LevelState : EcsGameState, IMessageReceiver
    {
        protected override void BeforeInit()
        {
            WM<WindowType>.Show(WindowType.LevelUI);
            WM<WindowType>.Show(WindowType.Level);
            
            Model.Get.Set("PopitLevelCount", 3);
            Model.Get.Set("PopitLevelTaken", 0);
            Model.Get.Set("SpinsLeft", 999);

            Messenger.Get.EnableReceiver(this);
        }

        protected override void BeforeDestroy()
        {
            WM<WindowType>.Hide(WindowType.LevelUI);
            WM<WindowType>.Hide(WindowType.Level);
            
            Messenger.Get.DisableReceiver(this);
        }

        public void ReceiveMessage(MessageHandler message)
        {
            switch (message.Message)
            {
                case "SureGameOver":
                {
                    message.Received();
                    GSM<StateType>.Get.ChangeOn(StateType.MainMenu);
                    break;
                }

                case "OnSideMenuButton":
                {
                    message.Received();
                    GSM<StateType>.Get.Nest(StateType.SideMenu);
                    break;
                }
            }
        }

        public override void MovedForward()
        {
            Messenger.Get.DisableReceiver(this);
        }

        public override void MovedBack()
        {
            Messenger.Get.EnableReceiver(this);
        }
    }
}