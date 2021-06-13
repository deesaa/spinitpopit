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

        private EcsFilter<PopitRef> _popitFilter;
        private EcsFilter<SpinnerRef> _spinnerFilter;
        private EcsFilter<LevelRef> _levelFilter;

        public void Destroy()
        {
            foreach (int index in _levelFilter)
            {
                UnityEngine.Object.Destroy(_levelFilter.Get1(index).levelView.gameObject);
                _levelFilter.GetEntity(index).Destroy();
            }
            
            foreach (int index in _popitFilter)
            {
                _popitFilter.GetEntity(index).Destroy();
            }
            
            foreach (int index in _spinnerFilter)
            {
                _spinnerFilter.GetEntity(index).Destroy();
            }
        }
    }
}