using System;
using Client.Components;
using Client.States;
using Client.UnityComponents;
using Components;
using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class SpinnerAimSystem : IEcsRunSystem
    {
        private EcsFilter<SpinnerRef> _filter;
        private EcsFilter<InputEvent> _inputFilter;
        
        public void Run()
        {
            foreach (int index in _filter)
            {
                SpinnerRef spinnerRef = _filter.Get1(index);

                switch (spinnerRef.spinnerView.aimMethodType)
                {
                    case AimMethodType.AimOnSpin:
                        AimOnSpin(spinnerRef);
                        break;
                    case AimMethodType.AimOnTouch:
                        AimOnTouch(spinnerRef);
                        break;
                }
            }
        }

        public void AimOnSpin(SpinnerRef spinnerRef)
        {
            if (spinnerRef.isReleased)
            {
                spinnerRef.spinnerView.aimView.Show(false);
            }
            else
            {
                spinnerRef.spinnerView.aimView.Show(true);
                spinnerRef.spinnerView.aimView.SetAngle(spinnerRef.spinnerView.body.transform.rotation, true);
            }
        }

        public void AimOnTouch(SpinnerRef spinnerRef)
        {
            if (spinnerRef.isReleased)
            {
                spinnerRef.spinnerView.aimView.Show(false);
            }
            else
            {
                foreach (var index in _inputFilter)
                {
                    if (_inputFilter.Get1(index).InputType == InputType.Touch)
                    {
                        Vector3 touchPosition = _inputFilter.Get1(index).touchPoint;
                       // Vector2 spinnerPosition = spinnerRef.spinnerView.transform.position;

                       // Vector2 direction = touchPosition - spinnerPosition;

                      //  Debug.DrawRay(spinnerPosition, direction, Color.red, 3f);

                        //var rotation = Quaternion.LookRotation(direction, Vector3.down);

                       // spinnerRef.spinnerView.aimView.SetAngle(rotation, false);
                        
                        spinnerRef.spinnerView.aimView.arrowPivot.transform.up = touchPosition - spinnerRef.spinnerView.aimView.arrowPivot.transform.position;
                        
                        spinnerRef.spinnerView.aimView.Show(true);
                    }
                }
            }
        }
    }
}