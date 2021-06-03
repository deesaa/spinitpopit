using Client.Components;
using Client.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class ReleaseSpinnerSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<SpinnerRef> _spinnerFilter;
        private EcsFilter<InputEvent> _inputFilter;

        private GameConfiguration _gameConfig;
        
        public void Run()
        {
            bool isSpaceDown = false;
            
            foreach (int inputIndex in _inputFilter)
            {
                if (_inputFilter.Get1(inputIndex).InputType == InputType.Space)
                {
                    isSpaceDown = true;
                }
            }
            
            foreach (int spinnerIndex in _spinnerFilter)
            {
                ref SpinnerRef spinnerRef = ref _spinnerFilter.Get1(spinnerIndex);
                
                if (spinnerRef.spinTime >= _gameConfig.minSpinTimeForRelease && 
                    spinnerRef.isReleased == false &&
                    isSpaceDown == false)
                {
                    spinnerRef.isReleased = true;
                    spinnerRef.timeAfterRelease = 0f;
                    spinnerRef.timeOnRelease = spinnerRef.spinTime;
                    spinnerRef.currentDirection = spinnerRef.spinnerView.aimView.arrowPivot.up;
                }
                
                
                if (spinnerRef.spinTime <= _gameConfig.minSpinTimeForRelease && 
                    spinnerRef.isReleased &&
                    isSpaceDown == false)
                {
                    spinnerRef.isReleased = false;
                    spinnerRef.timeOnRelease = 0f;
                }
            }
        }
    }
}