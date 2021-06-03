using Client.UnityComponents;
using Leopotam.Ecs;

namespace Client.Systems
{
    public class SpinnerInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private GameData _gameData;
        
        public void Init()
        {
            EcsEntity entity = _world.NewEntity();
            ref SpinnerRef spinnerRef = ref entity.Get<SpinnerRef>();
            spinnerRef.spinnerView = _gameData.spinnerView;
            spinnerRef.spinTime = 0f;
            spinnerRef.spinnerView.entity = entity;
        }
    }
}