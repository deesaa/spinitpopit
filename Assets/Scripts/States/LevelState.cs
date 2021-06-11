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
        private EcsSystems _systems;
        private GameObject Observer;
        
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

            _systems = new EcsSystems(World);
            InjectIn(_systems);
            
            Observer = Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
            
            _systems
                .Add(new FitViewportInitSystem())
                .Add(new SpinnerInitSystem())
                .Add(new PopitInitSystem())

                .Add(new InputSystem())
                //.Add(new LevelResetSystem())

                .Add(new SpinSpinTimeSystem())
                .Add(new ReleaseSpinnerSystem())
                .Add(new SpinnerMoveSystem())
                .Add(new SpinnerRotateSystem())
                .Add(new SpinnerAimSystem())
                .Add(new PopitTriggerSpinnerSystem())
                
                .OneFrame<InputEvent>()
                .OneFrame<TriggerEvent>()
                .OneFrame<GameEvent>()
                
                .Init ();
        }

        public override void OnExit()
        {
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Destroy(Observer);

            _systems.Destroy();
        
            WM<WindowType>.HideWindow(WindowType.LevelUI);
            WM<WindowType>.HideWindow(WindowType.Level);
        }

        public override void Update()
        {
            _systems.Run();
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