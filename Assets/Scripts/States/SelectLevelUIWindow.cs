﻿using System;
using Client.ReactiveValues;
using JDS;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.States
{
    public class SelectLevelUIWindow : Window<WindowType, RValueType>
    {
        protected override void OnShow()
        {
            
        }

        protected override void OnHide()
        {
            
        }
    }

    public class MonoEntity
    {
        private EcsEntity _entity;
        private GameObject _gameObject;

    }
}