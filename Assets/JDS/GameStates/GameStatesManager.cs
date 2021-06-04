using System.Collections.Generic;
using UnityEngine;

namespace JDS
{
    public static class GameStatesManager<T> where T : System.Enum
    {
        private static Dictionary<string, IGameState> _gameStatesString
            = new Dictionary<string, IGameState>();

        private static Dictionary<T, IGameState> _gameStates
            = new Dictionary<T, IGameState>();

        private static IGameState currentState;
        public static T CurrentStateType { private set; get; }

        public static void RegisterState(T name, IGameState gameState)
        {
            _gameStates[name] = gameState;
        }

        public static void ChangeOn(T name)
        {
            if (_gameStates.ContainsKey(name))
            {
                currentState?.OnExit();
                currentState = _gameStates[name];
                CurrentStateType = name;
                currentState.OnEnter();
            }
            else
            {
                Debug.Log($"State name: {name} does not registered");
            }
        }

        public static void SendEvent(string name)
        {
            currentState?.OnEvent(name);
        }
    }
}