using UnityEngine;

namespace JDS
{
    public class IPoolable : MonoBehaviour
    {
        public bool isPooled;
        
        public GlobalPool parent;
        public void Restore() => parent.Restore(this);
    }
}
