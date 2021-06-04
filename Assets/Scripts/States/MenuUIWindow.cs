using System;
using DG.Tweening;
using JDS;
using UnityEngine;
using UnityEngine.UI;

namespace Client.States
{
    public class MenuUIWindow : Window<WindowTypes>
    {
        public Button startButton;

        private void Awake()
        {
            startButton.onClick.AddListener(OnStartButtonClick);
        }

        private void OnStartButtonClick()
        {
            GameStatesManager<StateTypes>.SendEvent("StartBtn");
        }

        protected override void OnShow()
        {
            
        }

        protected override void OnHide()
        {
            
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(OnStartButtonClick);
        }
    }
}