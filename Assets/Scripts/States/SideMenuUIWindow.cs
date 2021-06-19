using Client.ReactiveValues;
using Client.States;
using JDS;
using JDS.Messenger;
using UnityEngine;
using UnityEngine.UI;

namespace States
{
    public class SideMenuUIWindow : Window<WindowType, RValueType>
    {
        public Button backButton;
        public Button levelsButton;

        protected override void OnAwake()
        {
            backButton.onClick.AddListener(OnBackButton);
            levelsButton.onClick.AddListener(OnLevelsButton);
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
        }
    }
}
