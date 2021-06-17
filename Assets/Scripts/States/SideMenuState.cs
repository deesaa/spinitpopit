using Client.ReactiveValues;
using Client.States;
using JDS;
using JDS.Messenger;
using UnityEngine;

namespace States
{
    public class SideMenuState : EcsGameState<RValueType>
    {
        protected override void BeforeInit()
        {
            Subscribe(RValueType.OnBackBtn, OnBackBtn);
            WM<WindowType>.Show(WindowType.SideMenuUI);
            EnableObserver();
        }

        protected override void AfterDestroy()
        {
            WM<WindowType>.Hide(WindowType.SideMenuUI);
            DisableObserver();
            Dispose();
        }

        private void OnBackBtn(object value)
        {
            GSM<StateType>.Get.Unnest();
        }
    }
}