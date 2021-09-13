using Leopotam.Ecs;

namespace JDS
{
    public static class Model
    {
        public static readonly ReactiveCore<string> Get = new ReactiveCore<string>();
        
        private static EcsWorld _mainWorld;
        public static EcsWorld MainWorld
        {
            get
            {
                if (_mainWorld == null)
                    DebugLog.LogError("EntityBehaviour' EcsWorld is not set");
                return _mainWorld;
            }

            set
            {
                if(_mainWorld != null)
                    DebugLog.LogError("EntityBehaviour' EcsWorld is already set");
                _mainWorld = value;
            }
        }
    }
}