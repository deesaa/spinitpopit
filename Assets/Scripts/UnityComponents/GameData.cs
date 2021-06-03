using System.Collections.Generic;
using UnityEngine;

namespace Client.UnityComponents
{
    public class GameData : MonoBehaviour
    {
        public Transform root;
        public Camera mainCamera;
        
        public SpinnerView spinnerView;
        public PopitView popitView;

        public LevelUIView levelUIView;

        public List<PopitView> scenePopitList;
    }
}