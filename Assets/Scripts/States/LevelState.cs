using Client.ReactiveValues;
using Components;
using JDS;
using Leopotam.Ecs;

namespace Client.States
{
    public class LevelState : GameStateEcs
    {
        private EcsSystems _systems;
        
        
        public override void OnEnter()
        {
            WM<WindowType>.ShowWindow(WindowType.LevelUI);
            WM<WindowType>.ShowWindow(WindowType.Level);
            
            GRC<RValueType>.Set(RValueType.PopitLevelStats, new PopitLevelStats()
            {
                count = 3,
                taken = 0
            });
            
            GRC<RValueType>.Set(RValueType.SpinsLeft, 3);

            //World.NewEntity().Get<GameEvent>().gameEventType = GameEventType.LevelRestart;
            World.NewEntity().Get<GameEvent>().gameEventType = GameEventType.LoadLevel;
        }

        public override void OnExit()
        {
            WM<WindowType>.HideWindow(WindowType.LevelUI);
            WM<WindowType>.HideWindow(WindowType.Level);
        }

        public override void OnEvent(string name)
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