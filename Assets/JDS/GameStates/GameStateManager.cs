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
    /// NestedGameStateManager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GSM<T>
    {
        private readonly Dictionary<T, IGameState> _gameStates = new Dictionary<T, IGameState>();
        private IGameState _currentState;
        private static GSM<T> _instance;
        private Stack<GameStateElement<T>> _nestedStates = new Stack<GameStateElement<T>>();
        
        public static GSM<T> Get => _instance;
        public T CurrentStateType { protected set; get;}
        
        public GSM()
        {
            if (_instance == null)
                _instance = this;
            else
                DebugLog.LogWarning("Instance of GSM<T> already exists");
        }
        
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
                string stateName = _currentState == null ? "NULL_STATE" : $"{StatesQueueToString()}";
                DebugLog.Log($"{stateName} change on {name}", "STATE QUEUE");

                var state = _gameStates[name];
                _nestedStates.Push(new GameStateElement<T>(state, name));
                _currentState = state;
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
                DebugLog.LogWarning($"Current Queue: {StatesQueueToString()}");
                return;
            }

            if (_gameStates.ContainsKey(name))
            {
                string stateName = _currentState == null ? "NULL_STATE" : $"{StatesQueueToString()}";
                DebugLog.Log($"{stateName} nest {name}", "STATE QUEUE");
                
                var state = _gameStates[name];
                _nestedStates.Push(new GameStateElement<T>(state, name));
                _currentState?.MovedForward();
                _currentState = state;
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
                    
                    string stateName = _currentState == null ? "NULL_STATE" : $"{StatesQueueToString()}";
                    DebugLog.Log(stateName, "STATE QUEUE");
                }
                else
                {
                    DebugLog.LogWarning("Can't dequeue last queued state");
                    DebugLog.LogWarning(StatesQueueToString(), "STATE QUEUE");
                    break;
                }
            }
        }

        private string StatesQueueToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var stateElement in _nestedStates)
            {
                builder.Append($"{stateElement} -> ");
            }
            builder.Append("<--");
            return builder.ToString();
        }

        public void SendEvent(string name)
        {
            _currentState?.StateMessage(name);
        }
    }
    
    public struct GameStateElement<T>
    {
        private IGameState _gameState;
        private T name;

        public IGameState GameState => _gameState;

        public GameStateElement(IGameState gameState, T name)
        {
            _gameState = gameState;
            this.name = name;
        }

        public void Exit()
        {
            DebugLog.Log($"Exit state {name}", "STATE");
            
            _gameState.OnExit();
        }
        
        public void MovedBack()
        {
            _gameState.MovedBack();
        }

        public bool Equals(T other)
        {
            return name.Equals(other);
        }
        public override string ToString()
        {
            return name.ToString();
        }
    }
}