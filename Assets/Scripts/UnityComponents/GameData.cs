using System.Collections.Generic;
using UnityComponents;
using UnityEngine;
using UnityEngine.UI;

namespace Client.UnityComponents
{
    public class GameData : MonoBehaviour
    {
        public Transform root;
        public Camera mainCamera;

        public SpinnerView spinnerView;
        public PopitView popitView;
        public SelectLevelCellView selectLevelCellView;
        public GridLayoutGroup selectLevelCellsGrid;
        public SelectSpinnerCellView selectSpinnerCellView;
        public GridLayoutGroup selectSpinnerCellGrid;

        //public List<PopitView> scenePopitList;

        public Transform levelContainer;
        public List<LevelView> levelViews;

        public List<SpinnerView> spinnerViews;

        public LevelView selectedLevelView;

        public InputArea levelInputArea;
    }
}