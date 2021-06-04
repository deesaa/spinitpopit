using System;
using System.Collections;
using System.Threading;
using Client.Components;
using Client.States;
using Client.Systems;
using Client.UnityComponents;
using Components;
using JDS;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Networking;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {

        EcsWorld _world;
        EcsSystems _systems;
        
        public GameData gameData;
        public GameConfiguration gameConfig;

        void Start () {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            
            GSM<StateType>.RegisterState(StateType.MainMenu, new MainMenuState());
            GSM<StateType>.RegisterState(StateType.Level, new LevelState());
            
            _systems
                .Add(new FitViewportInitSystem())
                .Add(new SpinnerInitSystem())
                .Add(new PopitInitSystem())

                .Add(new InputSystem())
                .Add(new SpinSpinTimeSystem())
                .Add(new ReleaseSpinnerSystem())
                .Add(new SpinnerMoveSystem())
                .Add(new SpinnerRotateSystem())
                .Add(new SpinnerAimSystem())
                .Add(new PopitTriggerSpinnerSystem())
                .OneFrame<InputEvent>()
                .OneFrame<TriggerEvent>()
                .Inject(gameConfig)
                .Inject(gameData)
                .Init ();
            
            GSM<StateType>.ChangeOn(StateType.MainMenu);
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}