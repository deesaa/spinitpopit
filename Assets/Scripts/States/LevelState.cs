using System;
using Client.Components;
using Client.ReactiveValues;
using Client.Systems;
using Components;
using DG.Tweening;
using DG.Tweening.Core;
using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.States
{
    public class LevelState : EcsGameState
    {
        protected override void BeforeInit()
        {
            WM<WindowType>.Show(WindowType.LevelUI);
            WM<WindowType>.Show(WindowType.Level);
            
            GRC<RValueType>.Set(RValueType.PopitLevelStats, new PopitLevelStats()
            {
                count = 3,
                taken = 0
            });
            
            GRC<RValueType>.Set(RValueType.SpinsLeft, 3);
        }

        protected override void BeforeDestroy()
        {
            WM<WindowType>.Hide(WindowType.LevelUI);
            WM<WindowType>.Hide(WindowType.Level);
        }
        
        public override void StateMessage(string name)
        {
            switch (name)
            {
                case "ZeroSpinsLeft":
                {
                    GRC<RValueType>.Set(RValueType.NextState, StateType.MainMenu);
                    GSM<StateType>.Get.ChangeOn(StateType.Transition);
                    break;
                }
            }
        }
    }
}