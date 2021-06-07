﻿using JDS;
using Leopotam.Ecs;

namespace Client.States
{
    public class MainMenuState : GameStateEcs
    {
        public override void OnEnter()
        {
            WM<WindowType>.ShowWindow(WindowType.MainMenuUI);
        }

        public override void OnExit()
        {
            WM<WindowType>.HideWindow(WindowType.MainMenuUI);
        }

        public override void OnEvent(string name)
        {
            switch (name)
            {
                case "StartBtn":
                    GSM<StateType>.ChangeOn(StateType.Level);
                    break;
            }
        }

        
    }
}