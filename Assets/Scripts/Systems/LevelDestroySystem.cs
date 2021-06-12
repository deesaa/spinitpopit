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

        private EcsFilter<PopitRef> _filter;

        public void Destroy()
        {
            foreach (int index in _filter)
            {
                ref PopitRef popitRef = ref _filter.Get1(index);
                popitRef.isTaken = false;
                _filter.GetEntity(index).Destroy();
            }
        }
    }
}