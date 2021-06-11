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
        //EcsSystems _systems;
        
        public GameData gameData;
        public GameConfiguration gameConfig;
        public PlayerStats playerStats;

        void Start ()
        {
            //Application.targetFrameRate = 30;
                
            _world = new EcsWorld ();
            //_systems = new EcsSystems (_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
           // Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            playerStats.Load();
            
            GSM<StateType>.RegisterState(StateType.MainMenu, new MainMenuState(), _world);
            GSM<StateType>.RegisterState(StateType.Level, new LevelState(), _world)
                .Inject(gameData)
                .Inject(gameConfig)
                .Inject(playerStats);
            

            GSM<StateType>.ChangeOn(StateType.MainMenu);
        }

        void Update () {
            GSM<StateType>.Update();
            //_systems?.Run ();
        }

        void OnDestroy () {
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}