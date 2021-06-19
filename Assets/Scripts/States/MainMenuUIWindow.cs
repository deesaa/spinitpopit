using System;
using Client.ReactiveValues;
using DG.Tweening;
using JDS;
using JDS.Messenger;
using UnityEngine;
using UnityEngine.UI;

namespace Client.States
{
    public class MainMenuUIWindow : Window<WindowType, RValueType>
    {
        public Button startButton;

        protected override void OnAwake()
        {
            startButton.onClick.AddListener(OnStartButtonClick);
        }

        private void OnStartButtonClick()
        {
            Messenger.Get.SendMessage("StartBtn");
        }

        protected override void OnShow()
        {
            
        }

        protected override void OnHide()
        {
            
        }

        protected override void AfterDestroy()
        {
            startButton.onClick.RemoveListener(OnStartButtonClick);
        }
    }
}