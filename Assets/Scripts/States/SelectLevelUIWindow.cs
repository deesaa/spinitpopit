using System;
using Client.ReactiveValues;
using Components;
using JDS;
using JDS.Messenger;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Client.States
{
    public class SelectLevelUIWindow : Window<WindowType, string>
    {
        public Button backButton;

        protected override void OnAwake()
        {
            backButton.onClick.AddListener(OnBackButton);
        }
        
        private void OnBackButton()
        {
            Messenger.Get.SendMessage("OnBackButton");
        }

        protected override void AfterDestroy()
        {
            backButton.onClick.RemoveListener(OnBackButton);
        }
    }
}