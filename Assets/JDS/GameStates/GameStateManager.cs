using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Client.States;
using Leopotam.Ecs;
using UnityEngine;

namespace JDS
{
    /// <summary>
    /// GameStateManager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GSM<T>
    {
        private readonly Dictionary<T, IGameState> _gameStates = new Dictionary<T, IGameState>();
        private IGameState _currentState;
        private Stack<GameStateElement> _nestedStates = new Stack<GameStateElement>();
        
        public static GSM<T> Get = new GSM<T>();
        public T CurrentStateType { protected set; get;}
        
        public void Add(T name, IGameState gameState)
        {
            DebugLog.Log($"Register state {name}", "STATE");
            
            if(_gameStates.ContainsKey(name))
                DebugLog.LogWarning($"State with name {name} is already registered", "STATE");

            _gameStates[name] = gameState;
        }

        public void ChangeOn(T name)
        {
            if (_gameStates.ContainsKey(name))
            {
                while (_nestedStates.Count > 0)
                {
                    _nestedStates.Pop().Exit();
                }
                
                DebugLog.Log($"{StatesStackToString()} change on {name}", "STATE QUEUE");

                var state = _gameStates[name];
                _nestedStates.Push(new GameStateElement(state, name));
                _currentState = state;
                CurrentStateType = name;
                state.OnEnter();
            }
            else
            {
                DebugLog.Log($"State {name} is not registered", "STATE");
            }
        }

        public void Nest(T name)
        {
            if (_nestedStates.Any(x => x.Equals(name)))
            {
                DebugLog.LogWarning($"Can't enqueue {name} state, this state is already enqueued");
                DebugLog.LogWarning($"Current Queue: {StatesStackToString()}");
                return;
            }

            if (_gameStates.ContainsKey(name))
            {
                DebugLog.Log($"{StatesStackToString()} nest {name}", "STATE QUEUE");
                
                var state = _gameStates[name];
                _nestedStates.Push(new GameStateElement(state, name));
                _currentState?.MovedForward();
                _currentState = state;
                CurrentStateType = name;
                state.OnEnter();
            }
            else
            {
                DebugLog.Log($"State {name} is not registered", "STATE");
            }
        }

        public void Unnest(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                if (_nestedStates.Count >= 2)
                {
                    _nestedStates.Pop().Exit();
                    _nestedStates.Peek().MovedBack();
                    _currentState = _nestedStates.Peek().GameState;
                    CurrentStateType = _nestedStates.Peek().GetName();
                    
                    DebugLog.Log(StatesStackToString, "STATE QUEUE");
                }
                else
                {
                    DebugLog.LogWarning("Can't dequeue last queued state");
                    DebugLog.LogWarning(StatesStackToString(), "STATE QUEUE");
                    break;
                }
            }
        }

        private string StatesStackToString()
        {
            if (_currentState == null)
                return "NULL_STATE";
            
            
            StringBuilder builder = new StringBuilder();
            foreach (var stateElement in _nestedStates)
            {
                builder.Append($"{stateElement} -> ");
            }
            builder.Append("<--/");

            return builder.ToString();
        }

        private struct GameStateElement
        {
            private IGameState _gameState;
            private T name;
            public IGameState GameState => _gameState;

            public GameStateElement(IGameState gameState, T name)
            {
                _gameState = gameState;
                this.name = name;
            }

            public T GetName() => name;

            public void Exit()
            {
                DebugLog.Log($"Exit state {name}", "STATE");
                _gameState.OnExit();
            }
            
            public void MovedBack() => _gameState.MovedBack();
            public bool Equals(T other) => name.Equals(other);
            public override string ToString() => name.ToString();
        }
    }
}