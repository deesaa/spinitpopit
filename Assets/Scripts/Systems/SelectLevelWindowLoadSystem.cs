using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Client.UnityComponents;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    
    
    public class SelectLevelWindowLoadSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private GameData _gameData;
        private EcsWorld _world;
        private bool _isInitCancelRequest = false;
        private EcsFilter<SelectLevelCellRef> _filter;
        
        public void Init()
        {
             _isInitCancelRequest = false;
             CreateCells();
        }

        private async void CreateCells()
        {
            int levelIndex = 1;
            foreach (var levelView in _gameData.levelViews)
            {
                if(_isInitCancelRequest)
                    return;
                
                var cellView =  Object.Instantiate(_gameData.selectLevelCellView, _gameData.selectLevelCellsGrid.transform);
                cellView.SetLevelView(levelView);
                cellView.SetLevelIndex(levelIndex);

               levelIndex++;
                await Task.Yield();
            }
        }

        public void Destroy()
        {
            _isInitCancelRequest = true;
            foreach (var index in _filter)
            {
                ref Delete delete = ref _filter.GetEntity(index).Get<Delete>();
                delete.deleteDelay = 0.35f;
                delete.gameObject = _filter.Get1(index)._cellView.gameObject;
            }
        }
    }
}