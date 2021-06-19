using System;
using Client.ReactiveValues;
using Components;
using JDS;
using JDS.Messenger;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UnityComponents
{
    public class SelectLevelCellView : MonoBehaviour
    {
        public Button cellButton;
        
        [SerializeField] private Text levelIndex;
        
        public EcsEntity entity;

        private void Awake()
        {
            cellButton.onClick.AddListener(OnCellClick);
        }

        public void OnCellClick()
        {
            RC<RValueType>.Set(RValueType.SelectedLevelView, entity.Get<SelectLevelCellRef>()._levelView);
            Messenger.Get.SendMessage("OnLevelClick");
        }

        private void OnDestroy()
        {
            cellButton.onClick.RemoveListener(OnCellClick);
        }

        public void SetLevelIndex(int levelIndex)
        {
            this.levelIndex.text = $"Level {levelIndex}";
        }
    }
}