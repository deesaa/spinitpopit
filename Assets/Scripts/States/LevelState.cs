using Client.ReactiveValues;
using Components;
using JDS;

namespace Client.States
{
    public class LevelState : IGameState
    {
        public void OnEnter()
        {
            //WM<WindowType>.HideAll();
            WM<WindowType>.ShowWindow(WindowType.LevelUI);
            
            GRC<RValueType>.Set(RValueType.PopitLevelStats, new PopitLevelStats()
            {
                count = 3,
                taken = 0
            });
            
            GRC<RValueType>.Set(RValueType.SpinsLeft, 3);
        }

        public void OnExit()
        {
            WM<WindowType>.HideWindow(WindowType.LevelUI);
        }

        public void OnEvent(string name)
        {
            switch (name)
            {
                case "ZeroSpinsLeft":
                    GSM<StateType>.ChangeOn(StateType.MainMenu);
                    break;
            }
        }
    }
}