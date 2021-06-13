using UnityEngine;

namespace Client.UnityComponents
{
    public struct SpinnerRef
    {
        public SpinnerView spinnerView;
        
        public float spinTime;
        public float timeOnRelease;
        public float timeAfterRelease;
        
        public Vector2 currentDirection;
        
        public bool isReleased;
    }
}