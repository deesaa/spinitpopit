using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems.Observe
{
    public class GameOverConditionObserveSystem : EcsObserveSystem<string>
    {
        private EcsWorld _world;
        
        public override void Init()
        {
            Bind("SpinsLeft", OnSpinsLeftChanged, Model.Get);
        }

        private void OnSpinsLeftChanged(object value)
        {
            
        }
    }
}