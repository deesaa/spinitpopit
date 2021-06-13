using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.States;
using Leopotam.Ecs;
using UnityEngine;

namespace JDS
{
    /// <summary>
    /// Game State Manager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /*public class GSM<T>
    {
        private static GSM<T> _instance;
        public static GSM<T> Get => _instance;
        
        private  Dictionary<T, IGameState> _gameStates = new Dictionary<T, IGameState>();
        
        private IGameState _currentState;
        public T CurrentStateType { private set; get;}

        public GSM()
        {
            if (_instance == null)
                _instance = this;
            else
                Debug.LogWarning("Instance of GSM<T> is already exist");
        }

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
    }*/

    public abstract class GSM<T>
    {
        protected Dictionary<T, IGameState> _gameStates = new Dictionary<T, IGameState>();
        
        protected IGameState _currentState;
        public T CurrentStateType { protected set; get;}

        public void Add(T name, IGameState gameState)
        {
            
#if UNITY_EDITOR
            Debug.Log($"STATE: Register state {name}");
            
            if(_gameStates.ContainsKey(name))
                Debug.LogWarning($"STATE: State with name {name} is already registered");
#endif
            
            _gameStates[name] = gameState;
        }

        public abstract void ChangeOn(T name);
        
        public void SendEvent(string name)
        {
            _currentState?.StateMessage(name);
        }
    }

    /// <summary>
    /// FlattenGameStateManager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FGSM<T> : GSM<T>
    {
        private static FGSM<T> _instance;
        public static FGSM<T> Get => _instance;
        
        public FGSM()
        {
            if (_instance == null)
                _instance = this;
            else
                Debug.LogWarning("Instance of GSM<T> is already exist");
        }
        
        public override void ChangeOn(T name)
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
    }

    /// <summary>
    /// NestedGameStateManager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NGSM<T> : GSM<T>
    {
        private static NGSM<T> _instance;
        public static NGSM<T> Get => _instance;
        
        private Queue<GameStateElement<T>> _nestedStates = new Queue<GameStateElement<T>>();
        
        public NGSM()
        {
            if (_instance == null)
                _instance = this;
            else
                Debug.LogWarning("Instance of GSM<T> is already exist");
        }
        
        public override void ChangeOn(T name)
        {
#if UNITY_EDITOR
            string state = _currentState == null ? "NULL_STATE" : $"{StatesQueueToString()}";
            Debug.Log($"STATE QUEUE: {state} changing on {name}");
#endif
            
            if (_gameStates.ContainsKey(name))
            {
                while (_nestedStates.Count > 0)
                {
                    _nestedStates.Dequeue().Exit();
                }
                
                Enqueue(name);
            }
            
#if UNITY_EDITOR
            else
            {
                Debug.Log($"STATE: State {name} is not registered");
            }
#endif
        }

        public void Enqueue(T name)
        {
            if (_nestedStates.Any(x => x.Equals(name)))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Can't enqueue {name} state, this state is already enqueued");
                Debug.LogWarning($"Current Queue: {StatesQueueToString()}");
#endif
                return;
            }

            if (_gameStates.ContainsKey(name))
            {
                var state = _gameStates[name];
                _nestedStates.Enqueue(new GameStateElement<T>(state, name));
                _currentState = state;
                state.OnEnter();
            }
            
#if UNITY_EDITOR
            else
            {
                Debug.Log($"STATE: State {name} is not registered");
            }
#endif
        }

        public void Dequeue()
        {
            if (_nestedStates.Count >= 2)
            {
                _nestedStates.Dequeue().Exit();
            }
            else
            {
                Debug.LogWarning("Can't dequeue last queued state");
                Debug.LogWarning($"STATE QUEUE: {StatesQueueToString()}");
            }
        }

        private string StatesQueueToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var stateElement in _nestedStates)
            {
                builder.Append($"{stateElement} -> ");
            }
            builder.Append("<--;");
            return builder.ToString();
        }
    }
    
    public struct GameStateElement<T>
    {
        private IGameState _gameState;
        private T name;

        public GameStateElement(IGameState gameState, T name)
        {
            _gameState = gameState;
            this.name = name;
        }

        public void Exit()
        {
            Debug.Log($"STATE: Exit state {name}");
            _gameState.OnExit();
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