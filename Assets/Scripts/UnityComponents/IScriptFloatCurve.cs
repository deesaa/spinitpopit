using UnityEngine;

namespace UnityComponents
{
    public abstract class IScriptFloatCurve : ScriptableObject, IEvaluable<float, float>
    {
        public float Evaluate(float value)
        {
            return Evaluate_(value);
        }

        protected abstract float Evaluate_(float value);
    }

    
}