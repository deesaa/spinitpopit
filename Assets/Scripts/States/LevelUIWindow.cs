using Client.ReactiveValues;
using Components;
using JDS;
using UnityEngine.UI;

namespace Client.States
{
    public class LevelUIWindow : Window<WindowType, RValueType>
    {
        public Text popitCounter;
        public Text spinsLeftCounter;

        protected override void OnAwake()
        {
            Bind(RValueType.PopitLevelStats, OnLevelStatsChange);
            Bind(RValueType.SpinsLeft, OnSpinsLeftChange);
        }

        protected override void OnShow()
        {
            
        }

        protected override void OnHide()
        {
            
        }

        private void OnSpinsLeftChange()
        {
            int spinsLeft = GRC<RValueType>.Get<int>(RValueType.SpinsLeft);
            spinsLeft = spinsLeft > -1 ? spinsLeft : 0;
            spinsLeftCounter.text =
                $"Spins Left: {spinsLeft}";
        }
        
        private void OnLevelStatsChange()
        {
            PopitLevelStats popitLevelStats 
                = GRC<RValueType>.Get<PopitLevelStats>(RValueType.PopitLevelStats);
        
            popitCounter.text =
                $"Popits Taken: {popitLevelStats.taken} / {popitLevelStats.count}";
        }
    }
}