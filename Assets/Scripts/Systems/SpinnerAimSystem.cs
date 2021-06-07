using Client.States;
using Client.UnityComponents;
using Components;
using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class SpinnerAimSystem : EcsStateRunSystem<StateType>
    {
        private EcsFilter<SpinnerRef> _filter;
        
        protected override void OnRun()
        {
            foreach (int index in _filter)
            {
                SpinnerRef spinnerRef = _filter.Get1(index);

                if (spinnerRef.isReleased)
                {
                    spinnerRef.spinnerView.aimView.Show(false);
                }
                else
                {
                    spinnerRef.spinnerView.aimView.Show(true);
                    spinnerRef.spinnerView.aimView.SetAngle(spinnerRef.spinnerView.body.transform.rotation);
                }
            }
        }
    }
}