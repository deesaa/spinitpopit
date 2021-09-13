using Client.Components;
using Client.States;
using Client.UnityComponents;
using Components;
using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class SpinSpinTimeSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<SpinnerRef> _spinnerFilter;
        private EcsFilter<InputEvent> _inputFilter;

        private GameConfiguration _gameConfiguration;

        public void Run()
        {
            foreach (int spinnerIndex in _spinnerFilter)
            {
                if(_spinnerFilter.Get1(spinnerIndex).isReleased)
                    ProceedReleasedSpinner(spinnerIndex);
                else
                    ProceedNotReleasedSpinner(spinnerIndex);
            }
        }

        private void ProceedReleasedSpinner(int spinnerIndex)
        {
            ref SpinnerRef spinnerRef = ref _spinnerFilter.Get1(spinnerIndex);
            spinnerRef.timeAfterRelease += Time.deltaTime;
            
            var view = spinnerRef.spinnerView;
            
            float k = view.spinTimeAfterReleaseToSpinTime01.Evaluate(spinnerRef.timeAfterRelease/spinnerRef.timeOnRelease);
            spinnerRef.spinTime = spinnerRef.timeOnRelease * k;
        }
        
        private void ProceedNotReleasedSpinner(int spinnerIndex)
        {
            foreach (int inputIndex in _inputFilter)
            {
                if (_inputFilter.Get1(inputIndex).InputType == InputType.Space)
                {
                    ref SpinnerRef spinnerRef = ref _spinnerFilter.Get1(spinnerIndex);
                    
                    if(spinnerRef.spinTime < spinnerRef.spinnerView.maxSpinTime)
                        spinnerRef.spinTime += Time.deltaTime;
                }
            }
        }
    }
}