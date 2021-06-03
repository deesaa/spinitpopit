using UnityEngine;

namespace JDS
{
    public class Scaler : MonoBehaviour
    {
        public static float ScaleFactor { get; private set; } = 1;
        
        public Transform scaleRoot;

        private void Awake()
        {
            Camera camera = Camera.main;

            float initalAspect = 10f / 19f;
            float currentAspect = camera.aspect;

            ScaleFactor = currentAspect / initalAspect;

            if (ScaleFactor >= 1)
            {
                ScaleFactor = 1;
                return;
            }
            
            scaleRoot.localScale = new Vector3(ScaleFactor, ScaleFactor, 1);
        }
    }
}
