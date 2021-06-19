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
        private bool InitCancelRequest = false;
        private EcsFilter<SelectLevelCellRef> _filter;
        
        public void Init()
        {
             InitCancelRequest = false;
             CreateCells();
        }

        private async void CreateCells()
        {
            int levelIndex = 1;
            foreach (var levelView in _gameData.levelViews)
            {
                if(InitCancelRequest)
                    return;
                
                SelectLevelCellRef cellRef = new SelectLevelCellRef();
                var cellView =  Object.Instantiate(_gameData.selectLevelCellView, _gameData.selectLevelCellsGrid.transform);
                cellRef._cellView = cellView;
                cellRef._levelView = levelView;

                EcsEntity entity = _world.NewEntity();
                cellView.entity = entity;
                cellView.SetLevelIndex(levelIndex);
                entity.Replace(cellRef);

                levelIndex++;
                await Task.Yield();
            }
        }

        public void Destroy()
        {
            InitCancelRequest = true;
            foreach (var index in _filter)
            {
                ref Delete delete = ref _filter.GetEntity(index).Get<Delete>();
                delete.deleteDelay = 0.35f;
                delete.gameObject = _filter.Get1(index)._cellView.gameObject;
            }
        }
    }
}