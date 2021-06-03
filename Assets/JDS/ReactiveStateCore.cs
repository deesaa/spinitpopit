using System;
using System.Collections.Generic;
using UnityEngine;

namespace JDS
{
    /*public class ReactiveStateCore : ReactiveCore
    {
        public static ReactiveStateCore Instance { get; private set; }

        //private Dictionary<string, GameState> _states = new Dictionary<string, GameState>();

        public static GameState currentState;

        public override void Awake()
        {
            if (Instance == null) Instance = this;
        }
        
        public void Set(string key, object value, bool react = true)
        {
            base.Set(key, value, react);
        }

        public void React(string key, GameState state)
        {
            if(currentState.HasFlag(state))
                base.React(key);
        }

        public T Get<T>(string key, GameState state)
        {
            if(currentState.HasFlag(state))
                return base.Get<T>(key);
            return default;
        }

        public void SubscribeKey(string key, Action action)
        {
            base.SubscribeKey(key, action);
        }

        public void UnsubscribeKey(string key, Action action)
        {
            base.UnsubscribeKey(key, action);
        }

        public void UnsubscribeKeyAll(string key)
        {
            base.UnsubscribeKeyAll(key);
        }
    }

    [Flags]
    public enum GameState
    {
        InGame,
        Menu,
        All
    }*/
}