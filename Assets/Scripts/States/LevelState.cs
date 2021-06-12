using Client.Components;
using Client.ReactiveValues;
using Client.Systems;
using Components;
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

            World.NewEntity().Get<SystemEvent>().systemEventType = SystemEventType.LoadLevel;
        }

        protected override void AfterDestroy()
        {
            WM<WindowType>.Hide(WindowType.LevelUI);
            WM<WindowType>.Hide(WindowType.Level);
        }
        
        public override void StateMessage(string name)
        {
            switch (name)
            {
                case "ZeroSpinsLeft":
                    GSM<StateType>.Get.ChangeOn(StateType.MainMenu);
                    break;
            }
        }
    }
}