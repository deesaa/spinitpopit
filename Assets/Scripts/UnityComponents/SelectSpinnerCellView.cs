using Client.ReactiveValues;
using Components;
using JDS;
using JDS.Messenger;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace UnityComponents
{
    public class SelectSpinnerCellView : MonoBehaviour
    {
        public Button cellButton;
        
        public EcsEntity entity;

        [SerializeField] private Text spinnerIndex;
        
        private void Awake()
        {
            cellButton.onClick.AddListener(OnCellClick);
        }

        public void OnCellClick()
        {
            RC<RValueType>.Set(RValueType.SelectedSpinnerView, entity.Get<SelectSpinnerViewCellRef>().spinnerView);
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
    }
}
