using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

namespace UnityComponents
{
    [CreateAssetMenu(fileName = "FloatCurve", menuName = "Curves/FloatCurve", order = 0)]
    public class FloatCurveFunction : IScriptFloatCurve
    {
        public AnimationCurve animationCurve;
        public float multiplier = 1f;
        
        protected override float Evaluate_(float value)
        {
            return animationCurve.Evaluate(value) * multiplier;
        }
    }
    
    public class IEaseCurve : IScriptFloatCurve
    {
        public Ease easeType;
        
        protected override float Evaluate_(float value)
        {
            return DOVirtual.EasedValue(0f, 1f, value, easeType);
        }
    }
}