using UnityEngine;

namespace JDS
{
    public abstract class IWindowHideAction : ScriptableObject
    {
        public abstract void Apply(Transform windowContainer);
        public abstract void Break();
    }
}