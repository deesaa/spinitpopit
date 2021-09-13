using System.Threading.Tasks;
using Client.UnityComponents;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class SelectSpinnerWindowLoadSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private GameData _gameData;
        private EcsWorld _world;

        private EcsFilter<SelectSpinnerViewCellRef> _filter;

        private bool _isInitCancelRequested;
        
        public void Init()
        {

            _isInitCancelRequested = false;
            CreateCells();
        }
        
        public async void CreateCells()
        {
            int spinnerIndex = 1;
            foreach (var spinnerView in _gameData.spinnerViews)
            {
                if(_isInitCancelRequested)
                    return;

                var cellView = Object.Instantiate(_gameData.selectSpinnerCellView, _gameData.selectSpinnerCellGrid.transform);
                cellView.SetSpinnerIndex(spinnerIndex);
                cellView.SetSpinnerView(spinnerView);
                
                await Task.Yield();
            }
        }

        public void Destroy()
        {
            _isInitCancelRequested = true;
            
            foreach (var index in _filter)
            {
                Delete delete = new Delete();
                delete.deleteDelay = 0.35f;
                delete.gameObject = _filter.Get1(index).SelectSpinnerCellView.gameObject;
                _filter.GetEntity(index).Replace(delete);
            }
        }
    }
}