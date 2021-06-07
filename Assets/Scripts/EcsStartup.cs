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
        public PlayerStats playerStats;

        void Start ()
        {
            //Application.targetFrameRate = 30;
                
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            
            playerStats.Load();
            
            GSM<StateType>.RegisterState(StateType.MainMenu, new MainMenuState(), _world);
            GSM<StateType>.RegisterState(StateType.Level, new LevelState(), _world);
            
            _systems
                .Add(new FitViewportInitSystem())
                .Add(new SpinnerInitSystem())
                //.Add(new PopitInitSystem())

                .Add(new InputSystem())
                .Add(new LevelResetSystem())
                
                .Add(new LoadLevelSystem())

                .Add(new SpinSpinTimeSystem().AddState(StateType.Level))
                .Add(new ReleaseSpinnerSystem().AddState(StateType.Level))
                .Add(new SpinnerMoveSystem().AddState(StateType.Level))
                .Add(new SpinnerRotateSystem().AddState(StateType.Level))
                .Add(new SpinnerAimSystem().AddState(StateType.Level))
                .Add(new PopitTriggerSpinnerSystem().AddState(StateType.Level))
                
                
                .OneFrame<InputEvent>()
                .OneFrame<TriggerEvent>()
                .OneFrame<GameEvent>()
                
                .Inject(gameConfig)
                .Inject(gameData)
                .Inject(playerStats)
                
                .Init ();

            GSM<StateType>.ChangeOn(StateType.MainMenu);
        }

        void Update () {
            GSM<StateType>.Update();
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