using Components;
using Leopotam.Ecs;

namespace Client.Systems
{
    public class LevelResetSystem : IEcsRunSystem
    {
        private EcsFilter<PopitRef> _popitFilter;
        private EcsFilter<GameEvent> _gameEventFilter;
        
        public void Run()
        {
            foreach (int index in _gameEventFilter)
            {
                if (_gameEventFilter.Get1(index).gameEventType == GameEventType.LevelRestart)
                {
                    foreach (int popitIndex in _popitFilter)
                    {
                        ref PopitRef popitRef = ref _popitFilter.Get1(popitIndex);
                        popitRef.isTaken = false;
                        popitRef.popitView.Reset();
                    }
                }
            }
        }
    }
}