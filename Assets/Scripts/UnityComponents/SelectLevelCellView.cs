using System;
using Client.ReactiveValues;
using Components;
using JDS;
using JDS.BindECS;
using JDS.Messenger;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UnityComponents
{
    public class SelectLevelCellView : EntityBehaviour<SelectLevelCellRef>
    {
        public Button cellButton;

        [SerializeField] private Text levelIndex;

        private void Awake()
        {
            GetBaseComponent()._cellView = this;

            cellButton.onClick.AddListener(OnCellClick);
        }

        public void OnCellClick()
        {
            Model.Get.Set("SelectedLevelView", Entity.Get<SelectLevelCellRef>()._levelView);
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

        public void SetLevelView(LevelView levelView)
        {
            GetBaseComponent()._levelView = levelView;
        }
    }
}