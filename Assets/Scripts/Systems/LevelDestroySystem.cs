using Client.UnityComponents;
using Components;
using Leopotam.Ecs;

namespace Client.Systems
{
    public class LevelDestroySystem : IEcsDestroySystem
    {
        private EcsWorld _world;
        private GameData _gameData;
        private PlayerStats _playerStats;

        private EcsFilter<PopitRef>.Exclude<Delete> _popitFilter;
        private EcsFilter<SpinnerRef>.Exclude<Delete> _spinnerFilter;
        private EcsFilter<LevelRef>.Exclude<Delete> _levelFilter;

        public void Destroy()
        {
            foreach (int index in _levelFilter)
            {
                ref Delete delete = ref _levelFilter.GetEntity(index).Get<Delete>();
                delete.deleteDelay = 0.5f;
                delete.gameObject = _levelFilter.Get1(index).levelView.gameObject;
            }
            
            foreach (int index in _popitFilter)
            {
                _popitFilter.GetEntity(index).Get<Delete>().deleteDelay = 0f;
            }
            
            foreach (int index in _spinnerFilter)
            {
                _spinnerFilter.GetEntity(index).Get<Delete>().deleteDelay = 0f;
                _spinnerFilter.Get1(index).spinnerView.DisableInteraction();
            }
        }
    }
}