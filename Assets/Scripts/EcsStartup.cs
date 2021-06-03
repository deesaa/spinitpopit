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
            // void can be switched to IEnumerator for support coroutines

            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            
            GameStatesManager<StateTypes>.RegisterState(StateTypes.MainMenu, new MainMenuState());
            
            
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
            
            GameStatesManager<StateTypes>.ChangeOn(StateTypes.MainMenu);
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