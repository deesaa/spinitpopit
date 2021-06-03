using UnityEngine;

namespace Client.UnityComponents
{
    [CreateAssetMenu]
    public class GameConfiguration : ScriptableObject
    {
        public AnimationCurve spinTimeToRotateSpeed;
        public AnimationCurve spinTimeToSpeed;
        public float speedMultiplier;
        public AnimationCurve spinTimeAfterReleaseToSpinTime01;
        
        public float minSpinTimeForRelease;
        
    }
}