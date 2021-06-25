using Client.States;
using Client.UnityComponents;
using Components;
using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class SpinnerRotateSystem : IEcsRunSystem
    {
        private EcsFilter<SpinnerRef> _filter;
        private GameConfiguration _gameConfiguration;
        
        public void Run()
        {
            foreach (int index in _filter)
            {
                var view = _filter.Get1(index).spinnerView;
                
                float rotateSpeed = view.spinTimeToRotateSpeed.Evaluate(_filter.Get1(index).spinTime);
                rotateSpeed *= _filter.Get1(index).spinnerView.rotateMultiplier;
                
                _filter.Get1(index).spinnerView.body.transform
                        .Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            }
        }
    }
}