using Client.ReactiveValues;
using Components;
using JDS;
using JDS.Messenger;
using UnityEngine;
using UnityEngine.UI;

namespace Client.States
{
    

    
    
    public class LevelUIWindow : Window<WindowType, string>
    {
        public Text popitStatsCounter;
        public Text spinsLeftCounter;

        public Button sideMenuBtn;

        protected override void OnAwake()
        {
            Bind("PopitLevelTaken", OnLevelStatsChange, Model.Get);
            Bind("PopitLevelCount", OnLevelStatsChange, Model.Get);
            Bind("SpinsLeft", OnSpinsLeftChange, Model.Get);
            
            
            
            sideMenuBtn.onClick.AddListener(OnSideMenuButton);
        }
        
        private void OnSpinsLeftChange(object value)
        {
            //int spinsLeft = RC<RValueType>.Get<int>(RValueType.SpinsLeft);
            //int spinsLeft = Model.Get.Get<int>("SpinsLeft");

            int spinsLeft = value.Get<int>();
            
            if(spinsLeft <= -1)
                Messenger.Get.SendSureMessage("SureGameOver");

            spinsLeft = spinsLeft > -1 ? spinsLeft : 0;
            spinsLeftCounter.text =
                $"Spins Left: {spinsLeft}";
        }
        
        private void OnLevelStatsChange(object value)
        {
           // PopitLevelStats popitLevelStats 
            //    = RC<RValueType>.Get<PopitLevelStats>(RValueType.PopitLevelStats);

            int popitLevelTaken = Model.Get.Get<int>("PopitLevelTaken");
            int popitLevelCount = Model.Get.Get<int>("PopitLevelCount");
        
            popitStatsCounter.text =
                $"Popits Taken: {popitLevelTaken} / {popitLevelCount}";
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