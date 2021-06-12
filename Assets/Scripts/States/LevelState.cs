using Client.Components;
using Client.ReactiveValues;
using Client.Systems;
using Components;
using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.States
{
    public class LevelState : GameStateEcs
    {
        public override void OnEnter()
        {
            WM<WindowType>.Show(WindowType.LevelUI);
            WM<WindowType>.Show(WindowType.Level);
            
            GRC<RValueType>.Set(RValueType.PopitLevelStats, new PopitLevelStats()
            {
                count = 3,
                taken = 0
            });
            
            GRC<RValueType>.Set(RValueType.SpinsLeft, 3);

            World.NewEntity().Get<SystemEvent>().systemEventType = SystemEventType.LoadLevel;
        }

        public override void OnExit()
        {
            WM<WindowType>.Hide(WindowType.LevelUI);
            WM<WindowType>.Hide(WindowType.Level);
        }
        public override void StateMessage(string name)
        {
            switch (name)
            {
                case "ZeroSpinsLeft":
                    GSM<StateType>.ChangeOn(StateType.MainMenu);
                    break;
            }
        }
    }
}