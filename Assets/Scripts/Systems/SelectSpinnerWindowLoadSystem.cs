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
        
        public void Init()
        {
            int index = 0;
            
            foreach (var spinnerView in _gameData.spinnerViews)
            {
                index++;
                SelectSpinnerViewCellRef cellRef = new SelectSpinnerViewCellRef();
                cellRef.spinnerView = spinnerView;

                var cellView = Object.Instantiate(_gameData.selectSpinnerCellView, _gameData.selectSpinnerCellGrid.transform);
                cellRef.SelectSpinnerCellView = cellView;
                cellView.SetSpinnerIndex(index);

                var entity = _world.NewEntity().Replace(cellRef);
                cellView.entity = entity;
            }
        }

        public void Destroy()
        {
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