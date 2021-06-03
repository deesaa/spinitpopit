using UnityEngine;

namespace Client.UnityComponents
{
    public struct SpinnerRef
    {
        public SpinnerView spinnerView;
        
        public float spinTime;
        public float timeOnRelease;
        public float timeAfterRelease;

        public float speedOnRelease;
        public float currentSpeed;

        public Vector2 currentDirection;

        //public bool canRecieveSpinTime;
        public bool isReleased;
    }
}