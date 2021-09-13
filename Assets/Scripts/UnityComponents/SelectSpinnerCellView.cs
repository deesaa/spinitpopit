using Client.ReactiveValues;
using Client.UnityComponents;
using Components;
using JDS;
using JDS.Messenger;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace UnityComponents
{
    public class SelectSpinnerCellView : EntityBehaviour<SelectSpinnerViewCellRef>
    {
        public Button cellButton;
        
        [SerializeField] private Text spinnerIndex;
        
        private void Awake()
        {
            GetBaseComponent().SelectSpinnerCellView = this;
            cellButton.onClick.AddListener(OnCellClick);
        }

        public void OnCellClick()
        {
            Model.Get.Set("SelectedSpinnerView", Entity.Get<SelectSpinnerViewCellRef>().spinnerView);
            Messenger.Get.SendMessage("OnSpinnerClick");
        }

        private void OnDestroy()
        {
            cellButton.onClick.RemoveListener(OnCellClick);
        }

        public void SetSpinnerIndex(int spinnerIndex)
        {
            this.spinnerIndex.text = $"Spinner {spinnerIndex}";
        }

        public void SetSpinnerView(SpinnerView spinnerView)
        {
            GetBaseComponent().spinnerView = spinnerView;
        }
    }
}
