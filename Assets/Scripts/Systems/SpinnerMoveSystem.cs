using Client.States;
using Client.UnityComponents;
using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class SpinnerMoveSystem : IEcsRunSystem
    {
        private GameConfiguration _gameConfiguration;
        private EcsFilter<SpinnerRef> _filter;

        public void Run()
        {
            foreach (int index in _filter)
            {
                if(!_filter.Get1(index).isReleased)
                    continue;

                var view = _filter.Get1(index).spinnerView;

                Vector3 moveDirection = _filter.Get1(index).currentDirection;
                float speed = view.spinTimeToSpeed.Evaluate(_filter.Get1(index).spinTime);
                speed *= view.speedMultiplier;
                _filter.Get1(index).spinnerView.rigidbody2D.velocity
                    = moveDirection * speed;
            }
        }
    }
}