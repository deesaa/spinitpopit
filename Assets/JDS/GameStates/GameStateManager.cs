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
    public class GSM<T>
    {
        private static GSM<T> _instance;

        public static GSM<T> Get
        {
            get
            {
                if (_instance == null)
                    _instance = new GSM<T>();
                return _instance;
            }
        }

        private  Dictionary<T, IGameState> _gameStates
            = new Dictionary<T, IGameState>();
        
        private IGameState _currentState;
        public T CurrentStateType { private set; get;}

        public void Add(T name, IGameState gameState)
        {
            
#if UNITY_EDITOR
            Debug.Log($"STATE: Register state {name}");
            
            if(_gameStates.ContainsKey(name))
                Debug.LogWarning($"STATE: State with name {name} is already registered");
#endif
            
            _gameStates[name] = gameState;
        }

        public void ChangeOn(T name)
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
        
        public void SendEvent(string name)
        {
            _currentState?.StateMessage(name);
        }
    }
}