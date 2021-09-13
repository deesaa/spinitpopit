using System;
using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.UnityComponents
{
    public abstract class EntityBehaviour<T> : MonoBehaviour where T : struct
    {

        private EcsEntity _entity;
        private bool _isEntityCreated;

        public EcsEntity Entity
        {
            get
            {
                if (!_isEntityCreated)
                {
                    _entity = Model.MainWorld.NewEntity();
                    _isEntityCreated = true;
                }
                return _entity;
            }
        }
        
        public ref T GetBaseComponent()
        { 
            ref T temp = ref Entity.Get<T>();
            return ref temp;
        }
    }
}