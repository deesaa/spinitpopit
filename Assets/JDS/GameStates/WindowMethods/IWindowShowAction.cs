using UnityEngine;

namespace JDS
{
    public abstract class IWindowShowAction : ScriptableObject
    {
        public abstract void SetStart(Transform windowContainer);
        public abstract void Apply(Transform windowContainer);
        public abstract void Break();
    }
}