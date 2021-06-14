using Client.ReactiveValues;
using Client.States;
using JDS;
using UnityEngine;
using UnityEngine.UI;

namespace States
{
    public class SideMenuUIWindow : Window<WindowType, RValueType>
    {
        public Button backButton;

        protected override void OnAwake()
        {
            backButton.onClick.AddListener(OnBackBtn);
        }

        public void OnBackBtn()
        {
            GSM<StateType>.Get.SendEvent("OnBackBtn");
        }

        protected override void AfterDestroy()
        {
            backButton.onClick.RemoveAllListeners();
        }
    }
}
