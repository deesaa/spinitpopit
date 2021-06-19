using Client.ReactiveValues;
using Components;
using JDS;
using JDS.Messenger;
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
            sideMenuBtn.onClick.AddListener(OnSideMenuButton);
        }
        
        private void OnSpinsLeftChange()
        {
            int spinsLeft = RC<RValueType>.Get<int>(RValueType.SpinsLeft);
            
            if(spinsLeft <= -1)
                Messenger.Get.SendSureMessage("SureGameOver");

            spinsLeft = spinsLeft > -1 ? spinsLeft : 0;
            spinsLeftCounter.text =
                $"Spins Left: {spinsLeft}";
        }
        
        private void OnLevelStatsChange()
        {
            PopitLevelStats popitLevelStats 
                = RC<RValueType>.Get<PopitLevelStats>(RValueType.PopitLevelStats);
        
            popitStatsCounter.text =
                $"Popits Taken: {popitLevelStats.taken} / {popitLevelStats.count}";
        }

        private void OnSideMenuButton()
        {
            Messenger.Get.SendMessage("OnSideMenuButton");
        }

        protected override void AfterDestroy()
        {
            sideMenuBtn.onClick.RemoveAllListeners();
        }
    }
}