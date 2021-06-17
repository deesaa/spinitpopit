using System;
using System.Collections.Generic;
using Client.ReactiveValues;
using DG.Tweening;
using JDS;
using JDS.Messenger;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.States
{
    public class MainMenuState : EcsGameState<RValueType>
    {
        private bool _interactable = false;
        
        protected override void BeforeInit()
        {
            Subscribe(RValueType.OnStartBtn, OnStartBtn);
            Subscribe(RValueType.OnSideMenuBtn, OnSideMenuBtn);
            
            WM<WindowType>.Show(WindowType.MainMenuUI);

            DOVirtual.DelayedCall(0.3f, () =>
            {
                EnableObserver();
            });
            
        }

        protected override void AfterDestroy()
        {
            WM<WindowType>.Hide(WindowType.MainMenuUI);
            DisableObserver();
            Dispose();
        }

        private void OnStartBtn(object value)
        {
            GSM<StateType>.Get.ChangeOn(StateType.Level);
        }

        private void OnSideMenuBtn(object value)
        {
            GSM<StateType>.Get.Nest(StateType.SideMenu);
        }
    }
}