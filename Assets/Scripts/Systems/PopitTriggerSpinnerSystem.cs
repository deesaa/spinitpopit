using System;
using Client.ReactiveValues;
using Client.States;
using Client.UnityComponents;
using Components;
using JDS;
using Leopotam.Ecs;
using UnityEngine.EventSystems;

namespace Client.Systems
{
    public class PopitTriggerSpinnerSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<PopitRef, TriggerEvent> _filter;
        
        public void Run()
        {
            foreach (int index in _filter)
            {
                if(_filter.Get1(index).isTaken)
                    continue;

                _filter.Get1(index).popitView.OnTake();
                _filter.Get1(index).isTaken = true;

                RC<RValueType>.Change<PopitLevelStats>(RValueType.PopitLevelStats, stats =>
                {
                    stats.taken++;
                    return stats;
                });
            }
        }
    }
}