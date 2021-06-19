using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

/*

namespace JDS.NewRC
{
    public class RC<T>
    {
        public static RC<T> Get = new RC<T>();
        
        private List<GroupObservable<T>> _groupObservables = new List<GroupObservable<T>>();

        public RC<T> Add(GroupObservable<T> groupObservable)
        {
            if(!_groupObservables.Contains(groupObservable))
                _groupObservables.Add(groupObservable);
            return this;
            
        }

        public void Remove(GroupObservable<T> groupObservable)
        {
            if (_groupObservables.Contains(groupObservable))
            {
                _groupObservables.Find(x => x == groupObservable).Dispose();
                _groupObservables.Remove(groupObservable);
            }
        }

        public void Push(T valueType, object value)
        {
            foreach (var groupObservable in _groupObservables)
            {
                groupObservable.Push(valueType, value);
            }
        }

        public void Override(T valueType, object value)
        {
            foreach (var groupObservable in _groupObservables)
            {
                groupObservable.Override(valueType, value);
            }
        }
    }
    
    public class GroupObservable<T> : IDisposable
    {
        private readonly Dictionary<T, Queue<object>> _valueChangeQueue 
            = new Dictionary<T, Queue<object>>();
        private readonly Dictionary<T, List<Action<object>>> _subscribes 
            = new Dictionary<T, List<Action<object>>>();

        public string GroupName;

        private bool _enabled = false;

        public TV PeekLast<TV>(T valueType)
        {
            if (_valueChangeQueue.ContainsKey(valueType) && _valueChangeQueue[valueType].Count > 0) 
                return (TV) _valueChangeQueue[valueType].Peek();
            
            return default;
        }
        
        public void Subscribe(T valueType, Action<object> onChanged)
        {
            if (!_subscribes.ContainsKey(valueType))
            {
                _subscribes.Add(valueType, new List<Action<object>>() {onChanged});
            }
            else
            {
                _subscribes[valueType].Add(onChanged);
            }
        }

        //Пытаемся отправить изменение объекта по ключу в подписки этой группы на изменение этого объекта
        //Если подписок на изменение этого объекта нет - ничего не делаем
        //Если подписки есть и группа активная - отпаравляем новое изменение, потом начинаем отправлять
        //все из очереди, пока группа активная. Если вдруг неактивная - ретурним нахуй
        //Если подписки есть и группа неактивая - добавляем в локальную очередь изменений объекта по ключу 
        
        public void Push(T valueType, object value)
        {
            if (!_subscribes.ContainsKey(valueType))
                return;

            if (!_valueChangeQueue.ContainsKey(valueType))
                _valueChangeQueue[valueType] = new Queue<object>();

            if (_enabled)
            {
                Send(valueType, value);
            }
            else
            {
                _valueChangeQueue[valueType].Enqueue(value);
                return;
            }

            while (_enabled && _valueChangeQueue[valueType].Count > 0)
            {
                Send(valueType, _valueChangeQueue[valueType].Dequeue());
            }
        }

        public void Override(T valueType, object value)
        {
            if (!_subscribes.ContainsKey(valueType))
                return;
            
            Debug.Log($"_subscribes Count: {valueType} of {_subscribes[valueType].Count}");
            
            if (!_valueChangeQueue.ContainsKey(valueType))
                _valueChangeQueue[valueType] = new Queue<object>();
            
            if (_valueChangeQueue[valueType].Count > 0)
            {
                _valueChangeQueue[valueType].Clear();
            }
                
            _valueChangeQueue[valueType].Enqueue(value);
            
            if (_enabled)
            {
                Send(valueType, value);
            }
        }

        private void Send(T valueType, object value)
        {
            foreach (var subscribe in _subscribes[valueType])
            {
                subscribe.Invoke(value);
            }
        }

        public void EnableObserver() => _enabled = true;
        public void DisableObserver() => _enabled = false;

        public void Dispose()
        {
            _subscribes.Clear();
            _valueChangeQueue.Clear();
        }
    }

    /// <summary>
    /// Global Observer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class GO<T>
    {
        public static readonly GroupObservable<T> Get = new GroupObservable<T>();
        
        static GO()
        {
            Get.GroupName = "Global Observer";
            Get.EnableObserver();
            RC<T>.Get.Add(Get);
        }
    }

    public enum TestValue
    {
        Value_test,
        Value_test2
    }
}

*/