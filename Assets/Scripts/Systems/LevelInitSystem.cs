using Client.States;
using Client.UnityComponents;
using Components;
using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class LevelInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private GameData _gameData;
        private PlayerStats _playerStats;

        public void Init()
        {
            var levelPrefab = _gameData.levelViews[_playerStats.data.lastLevel];

            var level = Object.Instantiate(levelPrefab, Vector3.zero, Quaternion.identity, _gameData.levelContainer);
            level.transform.localPosition = Vector3.zero;

            _world.NewEntity().Get<LevelRef>().levelView = level;
            
            foreach (PopitView p in level.popitViews)
            {
                EcsEntity popitEntity = _world.NewEntity();
                popitEntity.Get<PopitRef>().popitView = p;
                p.entity = popitEntity;
            }

            var spinner = Object.Instantiate(_gameData.spinnerView, level.spinnerStartPoint.position,
                Quaternion.identity, level.transform);
            
            EcsEntity spinnerEntity = _world.NewEntity();
            ref SpinnerRef spinnerRef = ref spinnerEntity.Get<SpinnerRef>();
            spinnerRef.spinnerView = spinner;
            spinnerRef.spinTime = 0f;
            spinnerRef.spinnerView.entity = spinnerEntity;
            spinnerRef.isReleased = false;
            spinnerRef.timeAfterRelease = 0f;
            spinnerRef.timeOnRelease = 0f;
            spinnerRef.currentDirection = Vector2.up;
        }
    }
}