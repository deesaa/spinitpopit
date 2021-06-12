using System;
using Client.UnityComponents;
using Components;
using Leopotam.Ecs;

namespace Client.Systems
{
    public class PopitInitSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        private EcsFilter<SystemEvent> _filter;

        private GameData _gameData;
        private PlayerStats _playerStats;
        
        public void Run()
        {
            if (!_filter.Contains(SystemEventType.LoadLevel))
                return;
            
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