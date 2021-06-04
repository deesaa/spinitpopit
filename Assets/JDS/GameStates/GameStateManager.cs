using System;
using System.Collections.Generic;
using UnityEngine;

namespace JDS
{
    /// <summary>
    /// Game State Manager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class GSM<T> where T : Enum
    {
        private static Dictionary<T, IGameState> _gameStates
            = new Dictionary<T, IGameState>();
        
        private static IGameState currentState;
        public static T CurrentStateType { private set; get;}

        public static void RegisterState(T name, IGameState gameState)
        {
            
#if UNITY_EDITOR
            Debug.Log($"STATE: Register state {name}");
#endif
            
            _gameStates[name] = gameState;
        }

        public static void ChangeOn(T name)
        {
            
#if UNITY_EDITOR
            string state = currentState == null ? "NULL_STATE" : $"{CurrentStateType}";
            Debug.Log($"STATE: {state} changing on {name}");
#endif
            
            if (_gameStates.ContainsKey(name))
            {
                currentState?.OnExit();
                currentState = _gameStates[name];
                CurrentStateType = name;
                currentState.OnEnter();
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
            currentState?.OnEvent(name);
        }
    }
}