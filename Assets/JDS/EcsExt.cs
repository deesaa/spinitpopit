using JDS;
using Leopotam.Ecs;

namespace Client.Systems
{
    public static class EcsExt
    {
        /*public static bool Contains<T>(this EcsFilter<T> filter, object value) where T : struct
        {
            if (filter.IsEmpty())
                return false;
            
            bool contains = false;
            foreach (int index in filter)
            {
                if (filter.Get1(index).Equals(value))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }*/
        
        public static EcsGameState Add<T>(this GSM<T> gsm, T name, EcsGameState ecsGameState, EcsWorld world)
        {
            ecsGameState.SetWorld(world);
            gsm.Add(name, ecsGameState);
            return ecsGameState;
        }
    }
}