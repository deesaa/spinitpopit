﻿using System;
using Client.ReactiveValues;
using Client.States;
using Client.UnityComponents;
using Components;
using JDS;
using Leopotam.Ecs;
using UnityEngine.EventSystems;

namespace Client.Systems
{
    public class PopitTriggerSpinnerSystem : EcsStateRunSystem<StateType>
    {
        private EcsWorld _world;
        private EcsFilter<PopitRef, TriggerEvent> _filter;
        
        protected override void OnRun()
        {
            foreach (int index in _filter)
            {
                if(_filter.Get1(index).isTaken)
                    continue;

                _filter.Get1(index).popitView.OnTake();
                _filter.Get1(index).isTaken = true;

                GRC<RValueType>.Change<PopitLevelStats>(RValueType.PopitLevelStats, stats =>
                {
                    stats.taken++;
                    return stats;
                });
            }
        }
    }
}