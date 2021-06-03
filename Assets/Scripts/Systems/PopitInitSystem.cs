using System;
using Client.UnityComponents;
using Components;
using Leopotam.Ecs;

namespace Client.Systems
{
    public class PopitInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private GameData _gameData;
        
        public void Init()
        {
            foreach (PopitView p in _gameData.scenePopitList)
            {
                EcsEntity entity = _world.NewEntity();
                entity.Get<PopitRef>().popitView = p;
                p.entity = entity;
            }
        }
    }
}