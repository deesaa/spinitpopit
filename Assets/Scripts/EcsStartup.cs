using System;
using System.Collections;
using System.Threading;
using Client.Components;
using Client.ReactiveValues;
using Client.States;
using Client.Systems;
using Client.Systems.Observe;
using Client.UnityComponents;
using Components;
using JDS;
using JDS.Messenger;
using Leopotam.Ecs;
using States;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace Client {
    sealed class EcsStartup : MonoBehaviour 
    {

        private EcsWorld _world;
        private EcsSystems _systems;

        public GameData gameData;
        public GameConfiguration gameConfig;
        public PlayerStats playerStats;

        private void Awake()
        {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);

            Model.MainWorld = _world;
        }

        void Start ()
        {
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
            UnityReactiveCoreObserver.Create(Model.Get);
#endif
            playerStats.Load();
            
            gameData.levelInputArea.SetEcsWorld(_world);

            var mainMenuState = new MainMenuState();
            GSM<StateType>.Get.Add(StateType.MainMenu, mainMenuState, _world);


            var levelState = new LevelState();
            GSM<StateType>.Get.Add(StateType.Level, levelState, _world)
                .Add(new LevelInitSystem())
                .Add(new LevelDestroySystem())
                .Inject(gameData)
                .Inject(playerStats);

            var selectLevelState = new SelectLevelState();
            GSM<StateType>.Get.Add(StateType.SelectLevel, selectLevelState, _world)
                .Add((IEcsInitSystem) new SelectLevelWindowLoadSystem())
                .Inject(gameData);
                
                
            var selectSpinnerState = new SelectSpinnerState();
            GSM<StateType>.Get.Add(StateType.SelectSpinner, selectSpinnerState, _world)
                .Add((IEcsInitSystem) new SelectSpinnerWindowLoadSystem())
                .Inject(gameData);

            var sideMenuState = new SideMenuState();
            GSM<StateType>.Get.Add(StateType.SideMenu, sideMenuState, _world);
            
            GSM<StateType>.Get.Add(StateType.Transition, new TransitionState());

            Messenger.Get
                .Add(mainMenuState)
                .Add(levelState)
                .Add(selectLevelState)
                .Add(sideMenuState)
                .Add(selectSpinnerState);
            
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
                
                .Add(new GameOverConditionObserveSystem())
                
                .OneFrame<InputEvent>()
                .OneFrame<TriggerEvent>()

                .Inject(gameData)
                .Inject(gameConfig)
                .Inject(playerStats)
                
                .Init ();
           
           GSM<StateType>.Get.ChangeOn(StateType.MainMenu);
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