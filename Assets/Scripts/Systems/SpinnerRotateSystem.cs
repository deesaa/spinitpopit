using Client.UnityComponents;
using Components;
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
                float rotateSpeed = _gameConfiguration.spinTimeToRotateSpeed.Evaluate(_filter.Get1(index).spinTime);
                
                _filter.Get1(index).spinnerView.body.transform
                        .Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            }
        }
    }
}