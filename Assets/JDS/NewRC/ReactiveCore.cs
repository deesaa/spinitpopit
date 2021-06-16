using System;
using System.Collections.Generic;

namespace JDS.NewRC
{

    public class TestRC
    {
        public void Test()
        {
            ReactiveCore<TestValue> rc = new ReactiveCore<TestValue>();

            TestGameRc testGameRc = new TestGameRc();
            
            

        }
    }
    
    public class ReactiveCore<T>
    {
        //public Dictionary<T, RCObserver> StateObservers = new Dictionary<T, RCObserver>();
        
        public Dictionary<T, RCObservable<object>> Observables = new Dictionary<T, RCObservable<object>>();

        public void Subscribe<T2>(T valueType, Action<object> action)
        {
            Observables[valueType].Subscribe(new RCObserver<object>());
        }
    }
    

    public class RCObserver<T> : IObserver<T>
    {
        public bool isActive;
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(T value)
        {
            throw new NotImplementedException();
        }
    }

    public class RCObservable<T> : IObservable<T>
    {
        public IDisposable Subscribe(IObserver<T> observer)
        {
            return new Unsubscriber<T>();
        }
    }

    public class Unsubscriber<T> : IDisposable
    {
        public void Dispose()
        {
            
        }
    }

    public class TestGameRc : GroupObserver
    {
        public void Init()
        {
            
        }
    }

    public class GroupObserver
    {
        
    }

    public enum TestValue
    {
        Value
    }
}