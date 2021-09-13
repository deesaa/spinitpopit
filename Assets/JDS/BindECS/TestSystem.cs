using System;
using System.Collections.Generic;
using System.Dynamic;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace JDS.BindECS
{

    /*public static class Model
    {
        public static readonly RC<string> Get = new RC<string>();
    }*/
    
    /*
    public class TestSystem : IEcsInitSystem
    {
        List<Action<object>> onChange;
        
        public void dd()
        {
            dynamic p = new Model();
            p.Value = 33;
            

        }

        public void OnChange(int x)
        {
            
        }

        public void Init()
        {
            throw new NotImplementedException();
        }
    }
    
    

    public class PopitLevelTakenObserveSystem : Binder, IEcsInitSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsFilter<Rotate> _filter;
        
        public void Init()
        {
            Model.PopitLevelTaken.Bind(Bind(OnChanged));
        }
        
        private void OnChanged(object o)
        {
            var x = o.Get<int>();
            
            foreach (int index in _filter)
            {
                _filter.Get1Ref(index).Unref().rotateSpeed *= 1.5f;
            }
        }

        public void Destroy()
        {
            Model.PopitLevelTaken.Unbind()
        }
    }

    public class Binder
    {
        protected static dynamic Model = new Model();
        private List<Action<object>> _subscriptions = new List<Action<object>>();
        
        public Action<object> Bind(Action<object> onChanged)
        {
            _subscriptions.Add(onChanged);
            return onChanged;
        }
        
        

    }

    public struct BindPair
    {
        private object _value;
        private List<Action<object>> _subscriptions;

        public void Subscribe(Action<object> onChanged)
        {
            if (_subscriptions == null)
                _subscriptions = new List<Action<object>>();
                
            _subscriptions.Add(onChanged);
        }

        public void SetNext(object value)
        {
            _value = value;
                
            if(_subscriptions == null)
                return;
                
            foreach (var subscription in _subscriptions)
                subscription(_value);
        }
    }
    */
    
    
    
    /*class Model : DynamicObject
    {
        private Dictionary<string, BindPair> _values = new Dictionary<string, BindPair>();
        //private Dictionary<string, Func<object>> _subscriptions = new Dictionary<string, Func<object>>();
        

        // установка свойства
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            //_members[binder.Name] = value;

            return true;
        }
        
        // получение свойства
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            //if (_members.ContainsKey(binder.Name))
            {
            //    result = _members[binder.Name];
                return true;
            }
            return false;
        }
        // вызов метода
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (!_values.ContainsKey(binder.Name))
            {
                DebugLog.LogWarning($"Model does not contain key '{binder.Name}' to subscribe" , "Model");
                result = null;
                return false;
            }
            
            
            //dynamic method = _members[binder.Name];
           // result = method(args[0]);
           // return result != null;
           result = null;
           return false;
        }
    }*/
}