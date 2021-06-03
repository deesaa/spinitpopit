using Client.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class FitViewportInitSystem : IEcsInitSystem
    {
        public static float ScaleFactor { get; private set; } = 1;
        
        private GameData _gameData;

        public void Init()
        {
            Camera camera = Camera.main;

            float initalAspect = 10f / 19f;
            float currentAspect = _gameData.mainCamera.aspect;

            ScaleFactor = currentAspect / initalAspect;

            if (ScaleFactor >= 1)
            {
                ScaleFactor = 1;
                return;
            }
            
            _gameData.root.localScale = new Vector3(ScaleFactor, ScaleFactor, 1);
        }
    }
}