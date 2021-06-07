using JDS;
using UnityEngine;

namespace Client.UnityComponents
{
    public class PlayerStats : MonoBehaviour
    {
        public Data data;
        
        public void Load()
        {
            data = SaverLoader.Load<Data>(gameObject);
        }

        public class Data : SaveLoad
        {
            public int lastLevel;
            public override void SetDefault()
            {
                lastLevel = 0;
            }
        }
        
    }
}