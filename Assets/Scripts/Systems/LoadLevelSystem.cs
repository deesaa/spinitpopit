using Client.States;
using Client.UnityComponents;
using Components;
using JDS;
using Leopotam.Ecs;

namespace Client.Systems
{
    public class LoadLevelSystem : EcsStateRunSystem<StateType>
    {
        private EcsWorld _world;
        private EcsFilter<GameEvent> _filter;
        private GameData _gameData;
        private PlayerStats _playerStats;

        protected override void OnRun()
        {
            foreach (int index in _filter)
            {
                if (_filter.Get1(index).gameEventType == GameEventType.LoadLevel)
                {
                    var level = _gameData.levelViews[_playerStats.data.lastLevel];
                    
                    foreach (PopitView p in level.popitViews)
                    {
                        EcsEntity entity = _world.NewEntity();
                        entity.Get<PopitRef>().popitView = p;
                        p.entity = entity;
                    }
                }
            }
        }
    }
    
}