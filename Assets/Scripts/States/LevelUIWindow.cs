using Client.ReactiveValues;
using Components;
using JDS;
using UnityEngine;
using UnityEngine.UI;

namespace Client.States
{
    public class LevelUIWindow : Window<WindowType, RValueType>
    {
        public Text popitStatsCounter;
        public Text spinsLeftCounter;

        public Button sideMenuBtn;

        protected override void OnAwake()
        {
            Bind(RValueType.PopitLevelStats, OnLevelStatsChange);
            Bind(RValueType.SpinsLeft, OnSpinsLeftChange);
            sideMenuBtn.onClick.AddListener(OnSideMenuBtn);
        }
        
        private void OnSpinsLeftChange()
        {
            int spinsLeft = GRC<RValueType>.Get<int>(RValueType.SpinsLeft);
            
            if(spinsLeft <= -1)
                Messenger.Get.SendSureMessage("ZeroSpinsLeft");

            spinsLeft = spinsLeft > -1 ? spinsLeft : 0;
            spinsLeftCounter.text =
                $"Spins Left: {spinsLeft}";
        }
        
        private void OnLevelStatsChange()
        {
            PopitLevelStats popitLevelStats 
                = GRC<RValueType>.Get<PopitLevelStats>(RValueType.PopitLevelStats);
        
            popitStatsCounter.text =
                $"Popits Taken: {popitLevelStats.taken} / {popitLevelStats.count}";
        }

        private void OnSideMenuBtn()
        {
            Messenger.Get.SendMessage("OnSideMenuBtn");
        }

        protected override void AfterDestroy()
        {
            sideMenuBtn.onClick.RemoveAllListeners();
        }
    }
}