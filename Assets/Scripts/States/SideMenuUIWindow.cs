using Client.ReactiveValues;
using Client.States;
using JDS;
using JDS.Messenger;
using UnityEngine;
using UnityEngine.UI;

namespace States
{
    public class SideMenuUIWindow : Window<WindowType, string>
    {
        public Button backButton;
        public Button levelsButton;
        public Button spinnersButton;

        protected override void OnAwake()
        {
            backButton.onClick.AddListener(OnBackButton);
            levelsButton.onClick.AddListener(OnLevelsButton);
            spinnersButton.onClick.AddListener(OnSpinnersButton);
        }

        private void OnSpinnersButton()
        {
            Messenger.Get.SendMessage("OnSpinnersButton");   
        }

        private void OnLevelsButton()
        {
            Messenger.Get.SendMessage("OnLevelsButton");   
        }

        public void OnBackButton()
        {
            Messenger.Get.SendMessage("OnBackButton");
        }

        protected override void AfterDestroy()
        {
            backButton.onClick.RemoveAllListeners();
            levelsButton.onClick.RemoveAllListeners();
            spinnersButton.onClick.RemoveAllListeners();
        }
    }
}
