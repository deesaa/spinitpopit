using Client.UnityComponents;
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

                Vector3 moveDirection = _filter.Get1(index).currentDirection;
                float speed = _gameConfiguration.spinTimeToSpeed.Evaluate(_filter.Get1(index).spinTime);
                speed *= _gameConfiguration.speedMultiplier;
                _filter.Get1(index).spinnerView.rigidbody2D.velocity
                    = moveDirection * speed * Time.deltaTime;
            }
        }
    }
}