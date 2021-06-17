using Client.ReactiveValues;
using Client.States;
using JDS;
using JDS.Messenger;
using JDS.NewRC;
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
            RC<RValueType>.Get.Override(RValueType.OnBackBtn, true);
        }

        protected override void AfterDestroy()
        {
            backButton.onClick.RemoveAllListeners();
        }
    }
}
