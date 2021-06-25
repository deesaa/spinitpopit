using Client.ReactiveValues;
using Client.States;
using Components;
using JDS;
using JDS.Messenger;
using UnityEngine;
using UnityEngine.UI;

namespace States
{
    public class SelectSpinnerUIWindow : Window<WindowType, RValueType>
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