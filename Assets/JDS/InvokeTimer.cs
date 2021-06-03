using System;
using System.Collections.Generic;
using UnityEngine;

namespace JDS
{
    public class InvokeTimer : MonoBehaviour
    {
        public static InvokeTimer Instance { get; private set; }

        private List<TimedAction> _registered = new List<TimedAction>();
        
        private void Awake() => Instance = this;

        public TimedAction Create(float interval, float delay, Action action) =>
            new TimedAction(interval, delay, action);

        public TimedAction Create(float interval, float delay, int targetIterations, Action action) =>
            new TimedAction(interval, delay, targetIterations, action); 

        public void Register(TimedAction timedAction)
        {
            if (_registered.Contains(timedAction))
                return;
            
            _registered.Add(timedAction);
        }

        private void Update()
        {
            for (int i = 0; i < _registered.Count; i++)
                _registered[i].Update(Time.deltaTime);
        }

        public void Destroy(TimedAction timedAction)
        {
            if(_registered.Contains(timedAction))
                _registered.Remove(timedAction);
        }
    }

    public class TimedAction
    {
        private float _timePassed;
        private float _delay;
        private float _interval;

        private bool _loop;
        private int _targetIterations;
        
        private bool _delayDone;
        private bool _destroyed;

        private Action _action;
        private Action _onComplete;
        private Func<bool> _completeCondition;

        private int _iterationsDone;
        public int IterationsDone => _iterationsDone;

        private bool _pause; 
        public bool Pause
        {
            get => _pause;
            
            set
            {
                if (_iterationsDone >= _targetIterations)
                {
                    Debug.Log("Target iterations done. You can only restart");    
                    return;
                }
                _pause = value;
            }
        }

        public TimedAction(float interval, float delay, Action action)
        {
            _interval = interval;
            _delay = delay;
            _action = action;
            _loop = true;
            
            InvokeTimer.Instance.Register(this);
        }

        public TimedAction(float interval, float delay, int targetIterations, Action action)
        {
            _interval = interval;
            _delay = delay;
            _action = action;
            _targetIterations = targetIterations;
            _loop = false;
            
            InvokeTimer.Instance.Register(this);
        }
        
        public TimedAction(float interval, float delay, int targetIterations, Action action, Action onComplete)
        {
            _interval = interval;
            _delay = delay;
            _action = action;
            _onComplete = onComplete;
            _targetIterations = targetIterations;
            _loop = false;
            
            InvokeTimer.Instance.Register(this);
        }
        
        public TimedAction(float interval, float delay, int targetIterations, Action action, Action onComplete, Func<bool> completeCondition)
        {
            _interval = interval;
            _delay = delay;
            _action = action;
            _onComplete = onComplete;
            _completeCondition = completeCondition;
            _targetIterations = targetIterations;
            _loop = false;
            
            InvokeTimer.Instance.Register(this);
        }

        public void Update(float deltaTime)
        {
            if (_completeCondition != null && _completeCondition())
            {
                _pause = true;
                _onComplete();
            }
            
            if(_pause) return;

            _timePassed += deltaTime;

            if (_timePassed >= _delay && !_delayDone)
            {
                Invoke();
                _delayDone = true;
            }
            
            if(!_delayDone) return;
            
            if (_timePassed >= _interval)
            {
                Invoke();
            }

            if (_loop) return;

            if (_iterationsDone >= _targetIterations)
            {
                _pause = true;
                _onComplete();
            }
        }

        private void Invoke()
        {
            if (_destroyed)
            {
                Debug.Log("Action is destroyed");
                return;
            }
            _timePassed = 0;
            _action();
            _iterationsDone++;
        }

        public void Restart()
        {
            _iterationsDone = 0;
            _timePassed = 0;
            _pause = false;
            _delayDone = false;
        }

        public void Destroy()
        {
            _destroyed = true;
            InvokeTimer.Instance.Destroy(this);
        }
    }
}