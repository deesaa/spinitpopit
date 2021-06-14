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
using States;
using UnityEngine;
using UnityEngine.Networking;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {

        private EcsWorld _world;
        private EcsSystems _systems;
        private GSM<StateType> _gsm;
        
        public GameData gameData;
        public GameConfiguration gameConfig;
        public PlayerStats playerStats;
        
        void Start ()
        {
            //Application.targetFrameRate = 30;
                
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _gsm = new GSM<StateType>();

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            playerStats.Load();

            GSM<StateType>.Get.Add(StateType.MainMenu, new MainMenuState(), _world);

            GSM<StateType>.Get.Add(StateType.Level, new LevelState(), _world)
                .Add(new LevelInitSystem())
                .Add(new LevelDestroySystem())
                .Inject(gameData)
                .Inject(playerStats);

            GSM<StateType>.Get.Add(StateType.SelectLevel, new SelectLevelState(), _world);
            GSM<StateType>.Get.Add(StateType.Transition, new TransitionState());

            GSM<StateType>.Get.Add(StateType.SideMenu, new SideMenuState(), _world);

            // GSM<StateType>.Get.Add(StateType.SelectLevel, new SelectLevelState(), _world)
           //     .Add(new SelectLevelInitSystem);

           _systems
                .Add(new FitViewportInitSystem())

                .Add(new InputSystem())

                .Add(new SpinSpinTimeSystem())
                .Add(new ReleaseSpinnerSystem())
                .Add(new SpinnerMoveSystem())
                .Add(new SpinnerRotateSystem())
                .Add(new SpinnerAimSystem())
                .Add(new PopitTriggerSpinnerSystem())
                
                .Add(new DeleteSystem())
                
                .OneFrame<InputEvent>()
                .OneFrame<TriggerEvent>()
                .OneFrame<SystemEvent>()
                
                .Inject(gameData)
                .Inject(gameConfig)
                .Inject(playerStats)
                
                .Init ();

           GSM<StateType>.Get.ChangeOn(StateType.Level);
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_world != null) {
                _systems.Destroy();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}