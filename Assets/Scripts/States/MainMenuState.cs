using System;
using DG.Tweening;
using JDS;
using Leopotam.Ecs;

namespace Client.States
{
    public class MainMenuState : EcsGameState
    {
        private bool _interactable = false;
        
        protected override void BeforeInit()
        {
            _interactable = false;
            WM<WindowType>.Show(WindowType.MainMenuUI);

            DOVirtual.DelayedCall(0.3f, () =>
            {
                _interactable = true;
            });
        }

        protected override void AfterDestroy()
        {
            WM<WindowType>.Hide(WindowType.MainMenuUI);
        }

        public override void StateMessage(string name)
        {
            if(!_interactable)
                return;
            
            switch (name)
            {
                case "StartBtn":
                    GSM<StateType>.Get.ChangeOn(StateType.Level);
                    break;
            }
        }
    }
}