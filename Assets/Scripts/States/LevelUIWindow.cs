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

        protected override void OnAwake()
        {
            Bind(RValueType.PopitLevelStats, OnLevelStatsChange);
            Bind(RValueType.SpinsLeft, OnSpinsLeftChange);
        }

        private void OnSpinsLeftChange()
        {
            int spinsLeft = GRC<RValueType>.Get<int>(RValueType.SpinsLeft);
            
            if(spinsLeft <= -1)
                NGSM<StateType>.Get.SendEvent("ZeroSpinsLeft");

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
    }
}