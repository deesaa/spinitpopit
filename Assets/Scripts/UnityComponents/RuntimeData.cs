using System;

namespace Client.UnityComponents
{
    public static class RuntimeData
    {
        public static Action<PopitLevelStats> OnPopitTakenChanged;
        public static Action<PopitLevelStats> OnPopitCountChanged;
        public static Action<int> OnSpinsLeftChanged;
        
        
        private static int _spinsLeft;
        public static int SpinsLeft
        {
            get => _spinsLeft;
            set
            {
                _spinsLeft = value;
                OnSpinsLeftChanged(value);
            }
        }
        
        private static int _popitTaken;
        public static int PopitTaken
        {
            get => _popitTaken;
            set
            {
                _popitTaken = value;
                OnPopitTakenChanged(new PopitLevelStats()
                {
                    count =  _popitCount,
                    taken = _popitTaken
                });
            }
        }
        
        private static int _popitCount;
        public static int PopitCount
        {
            get => _popitCount;
            set
            {
                _popitCount = value;
                OnPopitCountChanged(new PopitLevelStats()
                {
                    count = _popitCount,
                    taken = _popitTaken
                });
            }
        }
    }

    public struct PopitLevelStats
    {
        public int count;
        public int taken;
    }
}