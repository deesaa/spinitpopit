using Client.ReactiveValues;
using Components;
using JDS;
using JDS.Messenger;
using JDS.NewRC;
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
            Subscribe(RValueType.PopitLevelStats, OnLevelStatsChange);
            Subscribe(RValueType.SpinsLeft, OnSpinsLeftChange);
            sideMenuBtn.onClick.AddListener(OnSideMenuBtn);
        }
        
        private void OnSpinsLeftChange(object value)
        {
            int spinsLeft = (int) value;

            if (spinsLeft <= -1)
            {
                RC<RValueType>.Get.Override(RValueType.GameOver, true);
            }
            
            spinsLeft = spinsLeft > -1 ? spinsLeft : 0;
            spinsLeftCounter.text =
                $"Spins Left: {spinsLeft}";
        }
        
        private void OnLevelStatsChange(object levelStats)
        {
            PopitLevelStats popitLevelStats = (PopitLevelStats) levelStats;
        
            popitStatsCounter.text =
                $"Popits Taken: {popitLevelStats.taken} / {popitLevelStats.count}";
        }

        private void OnSideMenuBtn()
        {
            RC<RValueType>.Get.Override(RValueType.OnSideMenuBtn, true);
        }

        protected override void AfterDestroy()
        {
            sideMenuBtn.onClick.RemoveAllListeners();
        }
    }
}