﻿using Client.States;
using Client.UnityComponents;
using Components;
using JDS;
using Leopotam.Ecs;

namespace Client.Systems
{
    public class LevelInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private GameData _gameData;
        private PlayerStats _playerStats;

        public void Init()
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