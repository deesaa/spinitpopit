using Client.UnityComponents;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class LevelResetSystem : IEcsRunSystem
    {
        private EcsFilter<PopitRef> _popitFilter;
        private EcsFilter<SpinnerRef> _spinnerFilter;
        private EcsFilter<SystemEvent> _gameEventFilter;

        private GameData _gameData;
        private PlayerStats _playerStats;
        
        public void Run()
        {
            foreach (int index in _gameEventFilter)
            {
                if (_gameEventFilter.Get1(index).systemEventType == SystemEventType.LoadLevel)
                {
                    foreach (int popitIndex in _popitFilter)
                    {
                        ref PopitRef popitRef = ref _popitFilter.Get1(popitIndex);
                        popitRef.isTaken = false;
                        popitRef.popitView.Reset();
                    }

                    foreach (int spinnerIndex in _spinnerFilter)
                    {
                        var level = _gameData.levelViews[_playerStats.data.lastLevel]; 
                        
                        
                        ref SpinnerRef spinnerRef = ref _spinnerFilter.Get1(spinnerIndex);
                        spinnerRef.spinnerView.transform.position 
                            = level.spinnerStartPoint.position;
                          //  = _gameData.spinnerStartPoint.position;
                        spinnerRef.currentSpeed = 0f;
                        spinnerRef.isReleased = false;
                        spinnerRef.spinTime = 0f;
                        spinnerRef.timeAfterRelease = 0f;
                        spinnerRef.timeOnRelease = 0f;
                        spinnerRef.currentDirection = Vector2.up;
                    }
                }
            }
        }
    }
}