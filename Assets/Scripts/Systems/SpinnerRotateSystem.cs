using Client.States;
using Client.UnityComponents;
using Components;
using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class SpinnerRotateSystem : EcsStateRunSystem<StateType>
    {
        private EcsFilter<SpinnerRef> _filter;
        private GameConfiguration _gameConfiguration;
        
        protected override void OnRun()
        {
            foreach (int index in _filter)
            {
                float rotateSpeed = _gameConfiguration.spinTimeToRotateSpeed.Evaluate(_filter.Get1(index).spinTime);
                
                _filter.Get1(index).spinnerView.body.transform
                        .Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            }
        }
    }
}