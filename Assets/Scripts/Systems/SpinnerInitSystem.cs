
using Client.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class SpinnerInitSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private EcsWorld _world;
        private GameData _gameData;
        private PlayerStats _playerStats;

        private EcsFilter<SpinnerRef> _filter;
        
        public void Init()
        {
            EcsEntity entity = _world.NewEntity();
            ref SpinnerRef spinnerRef = ref entity.Get<SpinnerRef>();
            spinnerRef.spinnerView = _gameData.spinnerView;
            spinnerRef.spinTime = 0f;
            spinnerRef.spinnerView.entity = entity;
            
            var level = _gameData.levelViews[_playerStats.data.lastLevel]; 
            
            spinnerRef.spinnerView.transform.position 
                = level.spinnerStartPoint.position;
            spinnerRef.currentSpeed = 0f;
            spinnerRef.isReleased = false;
            spinnerRef.timeAfterRelease = 0f;
            spinnerRef.timeOnRelease = 0f;
            spinnerRef.currentDirection = Vector2.up;
        }

        public void Destroy()
        {
            foreach (int index in _filter)
            {
                _filter.GetEntity(index).Destroy();
            }
        }
    }
}