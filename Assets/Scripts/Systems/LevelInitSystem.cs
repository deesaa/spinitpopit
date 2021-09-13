using Client.ReactiveValues;
using Client.States;
using Client.UnityComponents;
using Components;
using JDS;
using JDS.BindECS;
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
            LevelView levelPrefab;
            
            var selectedLevel = Model.Get.Get<LevelView>("SelectedLevelView");
            if (selectedLevel == null)
                levelPrefab = _gameData.levelViews[0];
            else
                levelPrefab = selectedLevel;

            var level = Object.Instantiate(levelPrefab, Vector3.zero, Quaternion.identity, _gameData.levelContainer);
            level.transform.localPosition = Vector3.zero;

            _world.NewEntity().Get<LevelRef>().levelView = level;

            SpinnerView spinnerPrefab;
            var selectedSpinner = Model.Get.Get<SpinnerView>("SelectedSpinnerView");

            if (selectedSpinner == null)
                spinnerPrefab = _gameData.spinnerViews[0];
            else
                spinnerPrefab = selectedSpinner;
                
            
            var spinner = Object.Instantiate(spinnerPrefab, level.spinnerStartPoint.position,
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

            EcsEntity levelInputArea = _world.NewEntity();
            levelInputArea.Get<LevelInputAreaRef>().inputArea = _gameData.levelInputArea;
        }
    }
}