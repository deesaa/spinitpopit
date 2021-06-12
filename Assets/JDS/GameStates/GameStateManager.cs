using System;
using System.Collections.Generic;
using Client.States;
using Leopotam.Ecs;
using UnityEngine;

namespace JDS
{
    /// <summary>
    /// Game State Manager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class GSM<T>
    {
        private static Dictionary<T, IGameState> _gameStates
            = new Dictionary<T, IGameState>();
        
        private static IGameState _currentState;
        public static T CurrentStateType { private set; get;}
        
        public static void Add(T name, GameStateEcs gameState, EcsWorld world)
        {
            gameState.SetWorld(world);
            Add(name, gameState);
        }

        public static void Add(T name, IGameState gameState)
        {
            
#if UNITY_EDITOR
            Debug.Log($"STATE: Register state {name}");
            
            if(_gameStates.ContainsKey(name))
                Debug.LogWarning($"STATE: State with name {name} is already registered");
#endif
            
            _gameStates[name] = gameState;
        }

        public static void ChangeOn(T name)
        {
            
#if UNITY_EDITOR
            string state = _currentState == null ? "NULL_STATE" : $"{CurrentStateType}";
            Debug.Log($"STATE: {state} changing on {name}");
#endif
            
            if (_gameStates.ContainsKey(name))
            {
                _currentState?.OnExit();
                _currentState = _gameStates[name];
                CurrentStateType = name;
                _currentState.OnEnter();
            }
            
#if UNITY_EDITOR
            else
            {
                Debug.Log($"STATE: State {name} is not registered");
            }
#endif
        }
        
        public static void SendEvent(string name)
        {
            _currentState?.StateMessage(name);
        }
    }
}