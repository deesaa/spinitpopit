using System;
using Client.Components;
using Client.ReactiveValues;
using Client.Systems;
using Components;
using DG.Tweening;
using DG.Tweening.Core;
using JDS;
using JDS.Messenger;
using JDS.NewRC;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.States
{
    public class LevelState : EcsGameState<RValueType>
    {
        protected override void BeforeInit()
        {
            Subscribe(RValueType.GameOver, OnGameOver);
            Subscribe(RValueType.OnSideMenuBtn, OnSideMenuBtn);
            
            WM<WindowType>.Show(WindowType.LevelUI);
            WM<WindowType>.Show(WindowType.Level);
            
            RC<RValueType>.Get.Override(RValueType.PopitLevelStats, new PopitLevelStats()
            {
                count = 3,
                taken = 0
            });
            
            RC<RValueType>.Get.Override(RValueType.SpinsLeft, 3);
            
            EnableObserver();
        }

        protected override void BeforeDestroy()
        {
            WM<WindowType>.Hide(WindowType.LevelUI);
            WM<WindowType>.Hide(WindowType.Level);
            
            DisableObserver();
            Dispose();
        }

        private void OnGameOver(object value)
        {
            GSM<StateType>.Get.ChangeOn(StateType.MainMenu);
        }

        private void OnSideMenuBtn(object value)
        {
            GSM<StateType>.Get.Nest(StateType.SideMenu);
        }

        public override void MovedForward()
        {
            DisableObserver();
        }

        public override void MovedBack()
        {
            EnableObserver();
        }
    }
}